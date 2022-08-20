using Engine.Data;
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
    public class ActionEndMeleeActionProcessor : BattleActionProcessor<BattleActionAttackContext>
    {

        #region Properties

        /// <summary>
        ///     Завершение атаки
        ///     ---
        ///     Ending the attack
        /// </summary>
        public override CharacterBattleAction Action => CharacterBattleAction.EndMeleeAttack;

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
                        DoEndEdgedWeaponAction(handsController, context, character);
                        break;
                }
            }
        }

        #region Attack Type Methods

        private static void DoEndEdgedWeaponAction(HandsController handsController, BattleActionAttackContext context, LocationCharacter character)
        {
            var edged = context.Weapon as IEdgedWeapon;
            if (edged == null)
                return;

            switch (edged.WeaponType)
            {
                case WeaponType.Knife:
                    DoOneHandedAction(edged, context, character);
                    break;
                case WeaponType.TwoHanded:
                    DoTwoHandedAction(edged, context, character);
                    break;
            }
        }

        private static void DoOneHandedAction(IEdgedWeapon edgedWeapon, BattleActionAttackContext context, LocationCharacter character)
        {
            
            
            // BattleCalculationService.DoEdgedThrowAttack(character, edgedWeapon, context.WeaponPointPos);
        }

        private static void DoTwoHandedAction(IEdgedWeapon edgedWeapon, BattleActionAttackContext context, LocationCharacter character)
        {
            
            
            // BattleCalculationService.DoEdgedThrowAttack(character, edgedWeapon, context.WeaponPointPos);
        }

        #endregion

        public override void DoRollbackAction(BattleActionAttackContext context)
        { }

    }

}
