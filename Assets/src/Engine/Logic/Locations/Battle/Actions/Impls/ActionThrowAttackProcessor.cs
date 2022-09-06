using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие завершение атаки метания
    /// ---
    /// Processor performing the action of completing a throwing attack
    /// 
    /// </summary>
    public class ActionThrowActionProcessor : BattleActionProcessor<BattleActionAttackContext>
    {

        #region Properties

        /// <summary>
        ///     Завершение атаки метания
        ///     ---
        ///     Completing a Throwing Attack
        /// </summary>
        public override CharacterBattleAction Action => CharacterBattleAction.EndThrowAttack;

        #endregion

        public override void DoProcessAction(BattleActionAttackContext context)
        {
            var handsController = ObjectFinder.Find<HandsController>();
            var character = ObjectFinder.Find<LocationCharacter>();

            if (character.Weapon != null)
            {
                switch (character.Weapon.Type)
                {
                    case GroupType.WeaponEdged:
                        DoThrowEdgedWeaponAction(handsController, context, character);
                        break;
                    case GroupType.WeaponGrenade:
                        DoThrowGrenadeWeaponAction(handsController, context, character);
                        break;
                }
            }
        }

        #region Attack Type Methods

        /// <summary>
        ///     Инициирует процесс метания гранаты из руки в указанную точку
        ///     ---
        ///     Initiates the process of throwing the grenade from the hand to the specified point
        /// </summary>
        /// <param name="handsController">
        ///     Контроллер рук, в котором находятся выбранные оружия
        ///     ---
        ///     The hand controller, which contains the selected weapons
        /// </param>
        /// <param name="context">
        ///     Контекст атаки
        ///     ---
        ///     Context of the attack
        /// </param>
        /// <param name="character">
        ///     Персонаж, выполняющий атаку
        ///     ---
        ///     Character performing the attack
        /// </param>
        private static void DoThrowGrenadeWeaponAction(HandsController handsController, BattleActionAttackContext context, LocationCharacter character)
        {
            var grenade = context.Weapon as IGrenadeWeapon;
            if (grenade == null)
                return;

            BattleCalculationService.DoGrenadeAttack(character, grenade, context.WeaponPointPos);
            
            Game.Instance.Character.Inventory.RemoveByAddress(grenade); // Удаляем то что выкинули
            Game.Instance.Character.Equipment.TryRemoveItem(grenade); // Убираем из экипировки
            handsController.TryRemoveItem(grenade); // Убираем из рук
            if(handsController.Selected != null)
                handsController.Selected.UpdateCellInfo();
            character.EquipWeapon(null);
        }

        /// <summary>
        ///     Инициирует процесс метания метательного холодного оружия
        ///     ---
        ///     Initiates the process of throwing a throwing melee weapon
        /// </summary>
        /// <param name="handsController">
        ///     Контроллер рук, в котором находятся выбранные оружия
        ///     ---
        ///     The hand controller, which contains the selected weapons
        /// </param>
        /// <param name="context">
        ///     Контекст атаки
        ///     ---
        ///     Context of the attack
        /// </param>
        /// <param name="character">
        ///     Персонаж, выполняющий атаку
        ///     ---
        ///     Character performing the attack
        /// </param>
        private static void DoThrowEdgedWeaponAction(HandsController handsController, BattleActionAttackContext context, LocationCharacter character)
        {
            var edged = context.Weapon as IEdgedWeapon;
            if (edged == null)
                return;

            BattleCalculationService.DoEdgedThrowAttack(character, edged, context.WeaponPointPos);

            Game.Instance.Character.Inventory.RemoveByAddress(edged); // Удаляем то что выкинули
            Game.Instance.Character.Equipment.TryRemoveItem(edged); // Убираем из экипировки
            handsController.TryRemoveItem(edged); // Убираем из рук
            if(handsController.Selected != null)
                handsController.Selected.UpdateCellInfo();
            character.EquipWeapon(null);
        }

        #endregion

        public override void DoRollbackAction(BattleActionAttackContext context)
        { }

    }

}
