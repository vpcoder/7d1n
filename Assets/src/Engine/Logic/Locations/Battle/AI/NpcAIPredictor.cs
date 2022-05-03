using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Logic.Locations
{

    #pragma warning disable IDE0060

    public struct PredictorMoveResult
    {
        public IFirearmsWeapon Weapon;
        public bool Moved;
    }

    public class NpcAIPredictor
    {

        #region Singleton

        private static readonly Lazy<NpcAIPredictor> instance = new Lazy<NpcAIPredictor>(() => new NpcAIPredictor());
        public static NpcAIPredictor Instance { get { return instance.Value; } }
        private NpcAIPredictor() { }

        #endregion

        private BattleManager battle;

        /// <summary>
        /// Единая точка рассчитывающая стратегию группы AI
        /// </summary>
        public void CreateStrategy()
        {
            this.battle = ObjectFinder.Find<BattleManager>();

            Debug.Log("create strategy...");

            var currentGroup = Game.Instance.Runtime.BattleContext.OrderIndex;
            var allAiItems = NpcAISceneManager.Instance.GroupToNpcList; // Все НПС в своих группах
            var enemyList = allAiItems[currentGroup]; // Группа, которая сейчас ходит

            battle.EnemyGroupCounter = enemyList.Count;

            foreach (var enemy in enemyList)
                CreateStrategyForEnemy(currentGroup, enemy, allAiItems);
        }

        public void CreateStrategyForEnemy(EnemyGroup group, EnemyNpcBehaviour enemy, IDictionary<EnemyGroup, List<EnemyNpcBehaviour>> allAiItems)
        {
            Debug.Log("create strategy for '" + enemy.transform.name + "'...");

            var isNeedLook = enemy.Target == null;
            var target = enemy.Target ?? FindTarget(group, enemy, allAiItems); // Пытаемся найти цель
            if(target == null)
            {
                enemy.IsEndStep = true; // Не можем ходить, некого атаковать
                return;
            }

			enemy.Target = target;
            enemy.CurrentAction = null;
            enemy.NpcContext.Actions.Clear();
            enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.1f, 1f))); // Начинаем ход со случайной задержкой от 0.1 до 1 секунды, чтобы казалось что нпс более живые

            if (isNeedLook)
                enemy.NpcContext.Actions.Add(CreateLook(target, 1f));

            var ap = enemy.Enemy.AP;
            var moveResult = DoMoveIfNeeded(enemy, target, ref ap); // Движемся к цели, если нужно
            for(;;)
            {
                if (ap == 0)
                    break;

                if (moveResult.Weapon != null)
                {
                    if (!DoAttackOnlyRanged(enemy, target, moveResult.Weapon, ref ap)) // Атакуем дальнобойным пока есть ОД
                        break;
                }
                else
                {
                    if (!DoAttackIfNeeded(enemy, target, ref ap)) // Атакуем пока есть ОД
                        break;
                }
                enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.3f, 0.6f))); // 300mls-600mls
            }

            foreach(var action in enemy.NpcContext.Actions)
            {
                Debug.Log("+ " + enemy.transform.name + " action: " + action.Action.ToString());
            }

            enemy.DoNextAction(); // Начинаем первое действие
            enemy.IsEndStep = false; // Ходим
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


        private NpcBaseActionContext CreateWait(float delay)
        {
            return new NpcWaitActionContext()
            {
                Action = NpcActionType.Wait,
                WaitDelay = delay,
            };
        }

        private NpcBaseActionContext CreateMove(EnemyNpcBehaviour enemy, List<Vector3> path, float speed)
        {
            return new NpcMoveActionContext()
            {
                Action = NpcActionType.Move,
                Path = path,
                Speed = speed,
            };
        }

        private NpcBaseActionContext CreatePickWeapon(EnemyNpcBehaviour enemy, IWeapon weapon)
        {
            return new NpcPickWeaponActionContext()
            {
                Action = NpcActionType.PickWeapon,
                Weapon = weapon,
            };
        }

        private NpcBaseActionContext CreateAttack(EnemyNpcBehaviour enemy, IWeapon weapon)
        {
            return new NpcAttackActionContext()
            {
                Action = NpcActionType.Attack,
                Weapon = weapon,
            };
        }

        private NpcBaseActionContext CreateReload(EnemyNpcBehaviour enemy, IFirearmsWeapon weapon, IItem ammo)
        {
            return new NpcReloadActionContext()
            {
                Action = NpcActionType.Reload,
                FirearmsWeapon = weapon,
                Ammo = ammo,
            };
        }

        private NpcBaseActionContext CreateLook(IDamagedObject target, float speed)
        {
            return new NpcLookActionContext()
            {
                Action = NpcActionType.Look,
                Speed = speed,
                LookPoint = target.ToObject.transform.position,
            };
        }

    }

}
