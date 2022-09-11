using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Logic.Locations.Impls
{
    
    public struct PredictorMoveResult
    {
        public IFirearmsWeapon Weapon;
        public bool Moved;
    }
    
    /// <summary>
    ///
    /// Боевой предиктор
    /// ---
    /// Fighting Predictor
    /// 
    /// </summary>
    public class FightingPredictor : AbstractPredictorBase
    {

        /// <summary>
        ///     Работаем по состоянию "Битва"
        ///     ---
        ///     Working on the "Fighting" condition
        /// </summary>
        public override NpcStateType State => NpcStateType.Fighting;
        
        
        public override void CreateStrategyForNpcInner(PredictorContext context)
        {
            var group = context.EnemyGroup;
            var npc = context.Npc;
            var allAiItems = NpcAISceneManager.Instance.GroupToNpcList;
            
            Debug.Log("create strategy for '" + npc.transform.name + "'...");

            var isNeedLook = npc.Target == null;
            var target = npc.Target ?? FindTarget(group, npc, allAiItems); // Пытаемся найти цель
            if(target == null)
            {
                npc.IsEndStep = true; // Не можем ходить, некого атаковать
                return;
            }

            npc.Target = target;
            npc.CurrentAction = null;
            npc.NpcContext.Actions.Clear();
            npc.NpcContext.Actions.Add(CreateWait(Random.Range(0.1f, 1f))); // Начинаем ход со случайной задержкой от 0.1 до 1 секунды, чтобы казалось что нпс более живые

            if (isNeedLook)
                npc.NpcContext.Actions.Add(CreateLook(target, 1f));

            var ap = npc.Enemy.AP;
            var moveResult = DoMoveIfNeeded(npc, target, ref ap); // Движемся к цели, если нужно
            for(;;)
            {
                if (ap == 0)
                    break;

                if (moveResult.Weapon != null)
                {
                    if (!DoAttackOnlyRanged(npc, target, moveResult.Weapon, ref ap)) // Атакуем дальнобойным пока есть ОД
                        break;
                }
                else
                {
                    if (!DoAttackIfNeeded(npc, target, ref ap)) // Атакуем пока есть ОД
                        break;
                }
                npc.NpcContext.Actions.Add(CreateWait(Random.Range(0.3f, 0.6f))); // 300mls-600mls
            }

            foreach(var action in npc.NpcContext.Actions)
            {
                Debug.Log("+ " + npc.transform.name + " action: " + action.Action.ToString());
            }

            npc.DoNextAction(); // Начинаем первое действие
            npc.IsEndStep = false; // Ходим
        }

        private IDamagedObject FindTarget(EnemyGroup group, EnemyNpcBehaviour enemy, IDictionary<EnemyGroup, List<EnemyNpcBehaviour>> allAiItems)
        {
        	var potentialTargetList = new List<IDamagedObject>();

			foreach(var entry in allAiItems)
			{
				if(entry.Key == group)
					continue;

				foreach(var item in entry.Value)
				{
					var startPos = enemy.transform.position;
                    var path = enemy.CalculatePath(item.transform.position);
					if(path != null || TryFindRangedWeapon(item.Enemy.AP, item.Enemy.Weapons, item.Enemy.Items) != null)
					{
						potentialTargetList.Add(item.transform.GetComponent<IDamagedObject>());
					}
				}
			}
			
			if(potentialTargetList.Count == 0)
            	return null;

            potentialTargetList.Sort((o1, o2) => // Сортируем по расстоянию до цели
            {
                var v1 = Vector3.Distance(enemy.transform.position, o1.ToObject.transform.position);
                var v2 = Vector3.Distance(enemy.transform.position, o2.ToObject.transform.position);
                return Mathf.RoundToInt((v1 - v2) * 100);
            });

            return potentialTargetList[0]; // Берём в качестве цели самого ближнего противника
        }

		private IWeapon TryFindWeaponByPredicate(int ap, List<IWeapon> weapons, List<IItem> items, Func<IWeapon, bool> predicate = null)
		{
			if(weapons == null || weapons.Count == 0)
				return null;
			
			foreach(IWeapon weapon in weapons)
			{
				if(weapon.UseAP > ap)
					continue;

				if(predicate !=null && !predicate(weapon))
					continue;

				if(weapon.Type == GroupType.WeaponFirearms)
				{
					var firearms = (IFirearmsWeapon)weapon;
					if(firearms.AmmoCount > 0 || TryFindAmmo(firearms, items) != null)
						return weapon;
				}
				else
				{
					return weapon;
				}
			}
			return null;
		}

		private IItem TryFindAmmo(IFirearmsWeapon weapon, List<IItem> items)
		{
            if (Lists.IsEmpty(items))
                return null;
			return items.Where(item => item.ID == weapon.AmmoID).FirstOrDefault();
		}

		private IFirearmsWeapon TryFindRangedWeapon(int ap, List<IWeapon> weapons, List<IItem> items)
		{
			return (IFirearmsWeapon)TryFindWeaponByPredicate(ap, weapons, items, (w) =>  w.Type == GroupType.WeaponFirearms);			
		}

        private PredictorMoveResult DoMoveIfNeeded(EnemyNpcBehaviour enemy, IDamagedObject target, ref int ap)
        {
            var result = new PredictorMoveResult();

            if (ap <= 0)
                return result;

            var currentAp = ap;

            var weapon = TryFindRangedWeapon(ap, enemy.Enemy.Weapons, enemy.Enemy.Items);
            if (weapon != null) // FIXME: check raycast
            {
                result.Weapon = weapon;
                return result;
            }

			var endPos = target.ToObject.transform.position;
			var path = enemy.CalculatePath(endPos);

            if (Lists.IsEmpty(path))
                return result;

            if (path.Count == 0)
                return result;

            var pathFragment = new List<Vector3>();
			for(int i = 0; i < currentAp; i++)
			{
				if(path.Count <= i)
					break;

				ap--;
				pathFragment.Add(path[i]);
			}

            if (pathFragment.Count == 0)
                return result;

			enemy.NpcContext.Actions.Add(CreateMove(enemy, pathFragment, 1f));
            result.Moved = true;
            return result;
        }

        private bool DoAttackIfNeeded(EnemyNpcBehaviour enemy, IDamagedObject target, ref int ap)
        {
        	if(ap <= 0)
        		return false;
        	
            var weapon = TryFindWeaponByPredicate(ap, enemy.Enemy.Weapons, enemy.Enemy.Items); // Оружия которые можно использовать (хватает ОД)
            if (weapon == null)
                return false;

            switch(weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    enemy.NpcContext.Actions.Add(CreatePickWeapon(enemy, (IFirearmsWeapon)weapon));
                    enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.5f, 0.8f)));
                    enemy.NpcContext.Actions.Add(CreateAttack(enemy, (IFirearmsWeapon)weapon));
                    break;
                case GroupType.WeaponEdged:
                    enemy.NpcContext.Actions.Add(CreatePickWeapon(enemy, (IEdgedWeapon)weapon));
                    enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.5f, 0.8f)));
                    enemy.NpcContext.Actions.Add(CreateAttack(enemy, (IEdgedWeapon)weapon));
                    break;
                default:
                    throw new NotSupportedException();
            }

            ap -= weapon.UseAP;
            return true;
        }

        private bool DoAttackOnlyRanged(EnemyNpcBehaviour enemy, IDamagedObject target, IFirearmsWeapon weapon, ref int ap)
        {
            if (weapon == null)
                return false;

            switch (weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    if (weapon.AmmoCount > 0)
                    {
                        enemy.NpcContext.Actions.Add(CreatePickWeapon(enemy, weapon));
                        enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.5f, 0.8f)));
                        enemy.NpcContext.Actions.Add(CreateAttack(enemy, weapon));
                        weapon.AmmoCount--;
                    }
                    else
                    {
                        if (weapon.ReloadAP > ap)
                            return false;
                        var ammo = TryFindAmmo(weapon, enemy.Enemy.Items);
                        if (ammo != null)
                        {
                            ap -= weapon.ReloadAP;
                            enemy.NpcContext.Actions.Add(CreatePickWeapon(enemy, weapon));
                            enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.5f, 0.8f)));
                            enemy.NpcContext.Actions.Add(CreateReload(enemy, weapon, ammo));
                            return true;
                        }
                    }
                    ap -= weapon.UseAP;
                    return true;
                default:
                    throw new NotSupportedException();
            }
        }

    }
    
}