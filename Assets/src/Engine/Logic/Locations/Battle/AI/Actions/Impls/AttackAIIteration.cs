using Engine.Data;
using Engine.Logic.Locations.Animation;
using System;

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
    public class AttackAiIteration : AiIterationActionBase<NpcAttackActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.Attack;

        public override bool Iteration(CharacterNpcBehaviour npc, NpcAttackActionContext actionContext, float timestamp)
        {
            if (npc.Target == null) // Потеряли цель
                return true; // Конец этого действия

            npc.TargetAttackPos = npc.Target.ToObject.transform.position;

            switch (actionContext.Weapon.Type)
            {
                case GroupType.WeaponEdged:
                    npc.Animator.SetCharacterDoAttackType(AttackType.EdgedAttack);
                    BattleCalculationService.DoEdgedAttack(npc, npc.Target);
                    break;
                case GroupType.WeaponFirearms:
                    npc.Animator.SetCharacterDoAttackType(AttackType.SingleShot);
                    BattleCalculationService.DoFirearmsAttack(npc);
                    break;
                case GroupType.WeaponGrenade:
                    //BattleCalculationService.DoGrenadeAttack(npc, (IGrenadeWeapon)actionContext.Weapon);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return true;
        }

        public override void Start(CharacterNpcBehaviour npc, NpcAttackActionContext actionContext)
        { }

        public override void End(CharacterNpcBehaviour npc, NpcAttackActionContext actionContext, float timestamp)
        { }

    }

}
