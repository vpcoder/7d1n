using System;
using System.Collections.Generic;
using Engine.Data;
using UnityEngine;

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
    /// Основная задача - убить противников
    /// ---
    /// Fighting Predictor
    /// The main task is to kill the opponents
    /// 
    /// </summary>
    public class FightingPredictor : AbstractPredictorBase
    {

	    /// <summary>
	    ///     Название предиктора
	    ///		!Уникальное в рамках всех предикторов!
	    ///     ---
	    ///     Predictor name
	    ///		!Unique within all predictors!
	    /// </summary>
        public override string Name => "basic.human.fighting";
        
        
        public override void CreateStrategyForNpcInner(PredictorContext context)
        {
            var npc = context.Npc;
            var allAiItems = NpcAISceneManager.Instance.GroupToNpcList;
            
            Debug.Log("create strategy for '" + npc.transform.name + "'...");

            var isNeedLook = npc.Target == null;
            var target = npc.Target ?? FindTargetByDistancePriority(npc, allAiItems)?.DamagedObject; // Пытаемся найти цель
            if(target == null)
            {
                npc.StopNPC(); // Не можем ходить, некого атаковать, бой есть, врагов нет, чертовщина какая то...
                return;
            }

            npc.Target = target;
            npc.CurrentAction = null;
            npc.CharacterContext.Actions.Clear();
            npc.CharacterContext.Actions.Add(CreateWait(0.1f, 1f)); // Начинаем ход со случайной задержкой от 0.1 до 1 секунды, чтобы казалось что нпс более живые

            if (isNeedLook)
                npc.CharacterContext.Actions.Add(CreateLook(target, 1f));

            var ap = npc.Character.AP;
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
                npc.CharacterContext.Actions.Add(CreateWait(0.3f, 0.6f)); // 300mls-600mls
            }

            foreach(var action in npc.CharacterContext.Actions)
            {
                Debug.Log("+ " + npc.transform.name + " action: " + action.Action.ToString());
            }

            npc.StartNPC(); // Ходим
        }

        private PredictorMoveResult DoMoveIfNeeded(EnemyNpcBehaviour enemy, IDamagedObject target, ref int ap)
        {
            var result = new PredictorMoveResult();

            if (ap <= 0)
                return result;

            var currentAp = ap;

            var weapon = TryFindRangedWeapon(ap, enemy.Character.Weapons, enemy.Character.Items);
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

			enemy.CharacterContext.Actions.Add(CreateMove(enemy, pathFragment, 1f));
            result.Moved = true;
            return result;
        }

        private bool DoAttackIfNeeded(EnemyNpcBehaviour enemy, IDamagedObject target, ref int ap)
        {
        	if(ap <= 0)
        		return false;
        	
            var weapon = TryFindWeaponByPredicate(ap, enemy.Character.Weapons, enemy.Character.Items); // Оружия которые можно использовать (хватает ОД)
            if (weapon == null)
                return false;

            switch(weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    enemy.CharacterContext.Actions.Add(CreatePickWeapon(enemy, (IFirearmsWeapon)weapon));
                    enemy.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                    enemy.CharacterContext.Actions.Add(CreateAttack(enemy, (IFirearmsWeapon)weapon));
                    break;
                case GroupType.WeaponEdged:
                    enemy.CharacterContext.Actions.Add(CreatePickWeapon(enemy, (IEdgedWeapon)weapon));
                    enemy.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                    enemy.CharacterContext.Actions.Add(CreateAttack(enemy, (IEdgedWeapon)weapon));
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
                        enemy.CharacterContext.Actions.Add(CreatePickWeapon(enemy, weapon));
                        enemy.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                        enemy.CharacterContext.Actions.Add(CreateAttack(enemy, weapon));
                        weapon.AmmoCount--;
                    }
                    else
                    {
                        if (weapon.ReloadAP > ap)
                            return false;
                        var ammo = TryFindAmmo(weapon, enemy.Character.Items);
                        if (ammo != null)
                        {
                            ap -= weapon.ReloadAP;
                            enemy.CharacterContext.Actions.Add(CreatePickWeapon(enemy, weapon));
                            enemy.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                            enemy.CharacterContext.Actions.Add(CreateReload(enemy, weapon, ammo));
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