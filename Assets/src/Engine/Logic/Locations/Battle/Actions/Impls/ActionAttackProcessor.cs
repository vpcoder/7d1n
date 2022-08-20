using Engine.Data;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие атаки
    /// ---
    /// The processor performing the attack action
    /// 
    /// </summary>
    public class ActionActionProcessor : BattleActionProcessor<BattleActionAttackContext>
    {

        #region Properties

        /// <summary>
        ///     Атака
        ///     ---
        ///     Attack
        /// </summary>
        public override CharacterBattleAction Action => CharacterBattleAction.Attack;

        #endregion

        public override void DoProcessAction(BattleActionAttackContext context)
        {
            var controller = Controller;
            var handsController = ObjectFinder.Find<HandsController>();
            var handsActionsController = ObjectFinder.Find<HandsActionsController>();
            var character = ObjectFinder.Find<LocationCharacter>();

            if (context.Weapon != null)
            {
                context.WeaponPointPos = character.WeaponObject?.transform.position ?? Vector3.zero;
                character.Weapon = context.Weapon;
                switch (context.Weapon.Type)
                {
                    case GroupType.WeaponEdged:
                        DoAttackEdgedWeaponAction(context, controller, handsController, character);
                        break;
                    case GroupType.WeaponFirearms:
                        DoAttackFirearmsWeaponAction(context, controller, handsController, character);
                        break;
                    case GroupType.WeaponGrenade:
                        DoAttackGrenadeWeaponAction(context, controller, handsController, character);
                        break;
                }
            }

            handsActionsController.DoUnselectActions();

            if (context.AttackMarker != null)
                GameObject.Destroy(context.AttackMarker);
            context.AttackMarker = null;
        }

        #region Attack Type Methods

        private static void DoAttackGrenadeWeaponAction(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character)
        {
            if (context.Action == HandActionType.ThrowGrenade)
            {
                character.TargetAttackPos = context.AttackMarker.transform.position;
                // Запускаем анимацию, непосредственная атака пойдёт после её завершения
                ObjectFinder.Find<LocationCharacter>().Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.GrenadeThrow);
                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
                controller.Hide();
            }
        }

        private static void DoAttackFirearmsWeaponAction(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character)
        {
            var firearms = (IFirearmsWeapon)context.Weapon;
            if (context.Action == HandActionType.AttackFirearms)
            {
                TryFirearmsShot(context, controller, handsController, character, firearms);
            }
            if (context.Action == HandActionType.ReloadFirearms)
            {
                TryFirearmsReload(controller, handsController, firearms);
            }
        }

        private static void TryFirearmsReload(BattleActionsController controller, HandsController handsController, IFirearmsWeapon firearms)
        {
            long needAmmo = firearms.AmmoStackSize - firearms.AmmoCount;
            long haveAmmo = Game.Instance.Character.Inventory.HasCount(firearms.AmmoID);

            int valueAmmo = Mathf.Min((int)needAmmo, (int)haveAmmo);
            if (valueAmmo > 0)
            {
                AudioController.Instance.PlaySound(firearms.ReloadSoundType);
                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
                Game.Instance.Character.Inventory.Remove(firearms.AmmoID, valueAmmo);
                firearms.AmmoCount += valueAmmo;
                handsController.Selected?.UpdateCellInfo();
                controller.Hide();

                ObjectFinder.Find<LocationCharacter>().Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.Reload);
            }
        }

        private static void TryFirearmsShot(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character, IFirearmsWeapon firearms)
        {
            if (firearms.AmmoCount <= 0)
            {
                AudioController.Instance.PlaySound(firearms.JammingSoundType);
                return;
            }

            character.TargetAttackPos = context.AttackMarker.transform.position;

            BattleCalculationService.DoFirearmsAttack(character);
            firearms.AmmoCount--;
            handsController.Selected?.UpdateCellInfo();

            ObjectFinder.Find<LocationCharacter>().Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.SingleShot);

            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
            controller.Hide();
        }

        private static void DoAttackEdgedWeaponAction(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character)
        {
            if (context.Action == HandActionType.AttackEdged) // Атакуем ножом вблизи
            {
                // Запускаем анимацию, непосредственная атака пойдёт после её завершения
                ObjectFinder.Find<LocationCharacter>().Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.EdgedAttack);
                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
                controller.Hide();
            }

            if (context.Action == HandActionType.ThrowEdged) // Кидаем нож
            {
                character.TargetAttackPos = context.AttackMarker.transform.position;
                // Запускаем анимацию, непосредственная атака пойдёт после её завершения
                ObjectFinder.Find<LocationCharacter>().Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.EdgedThrow);
                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
                controller.Hide();
            }
        }

        #endregion

        public override void DoRollbackAction(BattleActionAttackContext context)
        {
            var controller = Controller;

            context.Weapon = null;
            if (context.AttackMarker != null)
                GameObject.Destroy(context.AttackMarker);
            context.AttackMarker = null;

            var handsActionsController = ObjectFinder.Find<HandsActionsController>();
            handsActionsController.DoUnselectActions();
            handsActionsController.HideActions();

            controller.NeedAP = 0;
            controller.Hide();
        }

    }

}
