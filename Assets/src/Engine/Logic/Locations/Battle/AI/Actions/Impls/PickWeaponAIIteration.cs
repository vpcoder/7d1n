using Engine.Logic.Locations.Animation;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Итератор действия выбора оружия.
    /// Итератор выполняет смену оружия для анимации NPC
    /// ---
    /// The iterator of the weapon selection action.
    /// The iterator performs a weapon change for the NPC animation
    /// 
    /// </summary>
    public class PickWeaponAiIteration : AiIterationActionBase<NpcPickWeaponActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.PickWeapon;

        public override bool Iteration(CharacterNpcBehaviour npc, NpcPickWeaponActionContext actionContext, float timestamp)
        {
            if (actionContext.Weapon != null)
            {
                npc.Animator.SetInteger(AnimationKey.WeaponEquipKey, (int)actionContext.Weapon.WeaponType);
            }
            npc.Weapon = actionContext.Weapon;
            return true;
        }

        public override void Start(CharacterNpcBehaviour npc, NpcPickWeaponActionContext actionContext)
        { }

        public override void End(CharacterNpcBehaviour npc, NpcPickWeaponActionContext actionContext, float timestamp)
        { }

    }

}
