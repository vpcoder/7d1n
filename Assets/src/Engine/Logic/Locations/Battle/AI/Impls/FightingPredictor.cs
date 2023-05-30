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

            
            if(Lists.IsEmpty(npc.CharacterContext.Actions))
                npc.StopNPC();
            else
                npc.StartNPC();
        }

        private PredictorMoveResult DoMoveIfNeeded(CharacterNpcBehaviour character, IDamagedObject target, ref int ap)
        {
            var result = new PredictorMoveResult();

            if (ap <= 0)
                return result;

            var currentAp = ap;

            var weapon = TryFindRangedWeapon(ap, character.Character.Weapons, character.Character.Items);
            if (weapon != null) // FIXME: check raycast
            {
                result.Weapon = weapon;
                return result;
            }

			var endPos = target.ToObject.transform.position;
			var path = character.CalculatePath(endPos);

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

			character.CharacterContext.Actions.Add(CreateMove(character, pathFragment, 1f));
            result.Moved = true;
            return result;
        }

        private bool DoAttackIfNeeded(CharacterNpcBehaviour character, IDamagedObject target, ref int ap)
        {
        	if(ap <= 0)
        		return false;
        	
            var weapon = TryFindWeaponByPredicate(ap, character.Character.Weapons, character.Character.Items); // Оружия которые можно использовать (хватает ОД)
            if (weapon == null)
                return false;

            switch(weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    character.CharacterContext.Actions.Add(CreatePickWeapon(character, (IFirearmsWeapon)weapon));
                    character.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                    character.CharacterContext.Actions.Add(CreateAttack(character, (IFirearmsWeapon)weapon));
                    break;
                case GroupType.WeaponEdged:
                    character.CharacterContext.Actions.Add(CreatePickWeapon(character, (IEdgedWeapon)weapon));
                    character.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                    character.CharacterContext.Actions.Add(CreateAttack(character, (IEdgedWeapon)weapon));
                    break;
                default:
                    throw new NotSupportedException();
            }

            ap -= weapon.UseAP;
            return true;
        }

        private bool DoAttackOnlyRanged(CharacterNpcBehaviour character, IDamagedObject target, IFirearmsWeapon weapon, ref int ap)
        {
            if (weapon == null)
                return false;

            switch (weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    if (weapon.AmmoCount > 0)
                    {
                        character.CharacterContext.Actions.Add(CreatePickWeapon(character, weapon));
                        character.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                        character.CharacterContext.Actions.Add(CreateAttack(character, weapon));
                        weapon.AmmoCount--;
                    }
                    else
                    {
                        if (weapon.ReloadAP > ap)
                            return false;
                        var ammo = TryFindAmmo(weapon, character.Character.Items);
                        if (ammo != null)
                        {
                            ap -= weapon.ReloadAP;
                            character.CharacterContext.Actions.Add(CreatePickWeapon(character, weapon));
                            character.CharacterContext.Actions.Add(CreateWait(0.5f, 0.8f));
                            character.CharacterContext.Actions.Add(CreateReload(character, weapon, ammo));
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