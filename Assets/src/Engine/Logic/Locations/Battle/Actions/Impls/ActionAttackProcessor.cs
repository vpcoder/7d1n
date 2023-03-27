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

        /// <summary>
        ///     Задача процессора - определить тип атаки, и для этого типа сделать соответствующие действия атаки
        ///     ---
        ///     The task of the processor is to determine the type of attack, and for this type to make appropriate attack actions
        /// </summary>
        /// <param name="context">
        ///     Контекст атаки
        ///     ---
        ///     Context of the attack
        /// </param>
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
                character.TargetAttackPos = context.AttackMarker?.transform.position ?? Vector3.zero;
                switch (context.Weapon.Type)
                {
                    case GroupType.WeaponEdged:
                        if (DataDictionary.Items.IsSystemHands(context.Weapon.ID)) // Голые руки?
                            DoAttackHandsAction(context, controller, handsController, character);
                        else // В руках что-то есть
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
                // Запускаем анимацию, непосредственная атака пойдёт после её завершения
                ObjectFinder.Find<LocationCharacter>().Animator.SetCharacterDoAttackType(AttackType.GrenadeThrow);
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

                ObjectFinder.Find<LocationCharacter>().Animator.SetCharacterDoAttackType(AttackType.Reload);
            }
        }

        private static void TryFirearmsShot(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character, IFirearmsWeapon firearms)
        {
            if (firearms.AmmoCount <= 0)
            {
                AudioController.Instance.PlaySound(firearms.JammingSoundType);
                return;
            }

            BattleCalculationService.DoFirearmsAttack(character);
            firearms.AmmoCount--;
            handsController.Selected?.UpdateCellInfo();

            ObjectFinder.Find<LocationCharacter>().Animator.SetCharacterDoAttackType(AttackType.SingleShot);

            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
            controller.Hide();
        }

        private static void DoAttackEdgedWeaponAction(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character)
        {
            if (context.Action == HandActionType.AttackEdged) // Атакуем ножом вблизи
            {
                // Запускаем анимацию, непосредственная атака пойдёт после её завершения
                character.Animator.SetCharacterDoAttackType(AttackType.EdgedAttack);
                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
                controller.Hide();
            }

            if (context.Action == HandActionType.ThrowEdged) // Кидаем нож
            {
                // Запускаем анимацию, непосредственная атака пойдёт после её завершения
                character.Animator.SetCharacterDoAttackType(AttackType.EdgedThrow);
                Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
                controller.Hide();
            }
        }

        private static void DoAttackHandsAction(BattleActionAttackContext context, BattleActionsController controller, HandsController handsController, LocationCharacter character)
        {
            // Запускаем анимацию, непосредственная атака пойдёт после её завершения
            character.Animator.SetCharacterDoAttackType(AttackType.HandsAttack);
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
            controller.Hide();
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
