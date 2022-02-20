using Engine.Data;
using Engine.Logic.Locations.Animation;
using System;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Итератор действия атаки оружием.
    /// Выполняет атаку из оружия по указанной цели
    /// ---
    /// Weapon attack action iterator.
    /// Executes a weapon attack on the specified target
    /// 
    /// </summary>
    public class AttackAIIteration : AIIterationActionBase<NpcAttackActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.Attack;

        public override bool Iteration(EnemyNpcBehaviour npc, NpcAttackActionContext actionContext, float timestamp)
        {
            if (npc.Target == null) // Потеряли цель
                return true; // Конец этого действия

            npc.Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.SingleShot);
            npc.TargetAttackPos = npc.Target.ToObject.transform.position;

            switch (actionContext.Weapon.Type)
            {
                case GroupType.WeaponEdged:
                    BattleCalculationService.DoEdgedAttack(npc, npc.Target);
                    break;
                case GroupType.WeaponFirearms:
                    BattleCalculationService.DoFirearmsAttack(npc);
                    break;
                case GroupType.WeaponGrenade:
                    BattleCalculationService.DoGrenadeAttack(npc);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return true;
        }

        public override void Start(EnemyNpcBehaviour npc, NpcAttackActionContext actionContext)
        { }

        public override void End(EnemyNpcBehaviour npc, NpcAttackActionContext actionContext, float timestamp)
        { }

    }

}
