using Engine.Logic.Locations.Animation;
using System;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Итератор действия перезарядки оружия.
    /// ---
    /// The iterator of the reload weapon action.
    /// 
    /// </summary>
    public class ReloadAiIteration : AiIterationActionBase<NpcReloadActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.Reload;

        public override bool Iteration(CharacterNpcBehaviour npc, NpcReloadActionContext actionContext, float timestamp)
        {
            var weapon = actionContext.FirearmsWeapon;
            var ammo = actionContext.Ammo;
            var ammoCount = Math.Min(ammo.Count, weapon.AmmoStackSize);
            weapon.AmmoCount += ammoCount;
            ammo.Count -= ammoCount;
            if (ammo.Count == 0)
            {
                npc.Character.Items.Remove(ammo);
            }

            AudioController.Instance.PlaySound(npc.AttackAudioSource, weapon.ReloadSoundType);
            npc.Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.Reload);

            return true;
        }

        public override void Start(CharacterNpcBehaviour npc, NpcReloadActionContext actionContext)
        { }

        public override void End(CharacterNpcBehaviour npc, NpcReloadActionContext actionContext, float timestamp)
        { }

    }

}
