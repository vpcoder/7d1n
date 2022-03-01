using Engine.Data;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие завершение атаки
    /// ---
    /// Processor performing the attack completion action
    /// 
    /// </summary>
    public class ActionThrowActionProcessor : BattleActionProcessor<BattleActionAttackContext>
    {

        #region Properties

        /// <summary>
        ///     Завершение атаки
        ///     ---
        ///     Ending the attack
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

        private static void DoThrowGrenadeWeaponAction(HandsController handsController, BattleActionAttackContext context, LocationCharacter character)
        {
            var grenade = context.Weapon as IGrenadeWeapon;
            if (grenade == null)
                return;

            BattleCalculationService.DoGrenadeAttack(character, grenade, context.WeaponPointPos);
            
            Game.Instance.Character.Inventory.RemoveByAddress(grenade); // Удаляем то что выкинули
            Game.Instance.Character.Equipment.TryRemoveItem(grenade); // Убираем из экипировки
            handsController.TryRemoveItem(grenade); // Убираем из рук
            handsController.Selected?.UpdateCellInfo();
            character.EquipWeapon(null);
        }

        private static void DoThrowEdgedWeaponAction(HandsController handsController, BattleActionAttackContext context, LocationCharacter character)
        {
            var edged = context.Weapon as IEdgedWeapon;
            if (edged == null)
                return;

            BattleCalculationService.DoEdgedThrowAttack(character, edged, context.WeaponPointPos);

            Game.Instance.Character.Inventory.RemoveByAddress(edged); // Удаляем то что выкинули
            Game.Instance.Character.Equipment.TryRemoveItem(edged); // Убираем из экипировки
            handsController.TryRemoveItem(edged); // Убираем из рук
            handsController.Selected?.UpdateCellInfo();
            character.EquipWeapon(null);
        }

        #endregion

        public override void DoRollbackAction(BattleActionAttackContext context)
        { }

    }

}
