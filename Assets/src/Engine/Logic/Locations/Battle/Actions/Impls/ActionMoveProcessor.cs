using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие перемещения персонажа
    /// ---
    /// The processor that performs the action of moving the character
    /// 
    /// </summary>
    public class ActionMoveProcessor : BattleActionProcessor<BattleActionMoveContext>
    {

        #region Properties

        /// <summary>
        ///     Перемещение
        ///     ---
        ///     Moving
        /// </summary>
        public override CharacterBattleAction Action => CharacterBattleAction.Move;

        #endregion

        public override void DoProcessAction(BattleActionMoveContext context)
        {
            var character = ObjectFinder.Find<LocationCharacter>();
            var controller = Controller;

            if (Lists.IsNotEmpty(character.Path))
            {
                DoRollbackAction(context);
                return; // Персонаж уже выполняет перемещение, не можем ничего делать, пока он не завершит операцию
            }
            
            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД
            
            character.SetPath(context.Points); // Перемещаемся

            ObjectFinder.Find<CharacterMoveVisializerController>().HidePath(); // Сбрасываем путь
            context.Points = null;

            controller.NeedAP = 0;
            controller.Hide();
        }

        public override void DoRollbackAction(BattleActionMoveContext context)
        {
            var controller = Controller;

            controller.NeedAP = 0;
            ObjectFinder.Find<CharacterMoveVisializerController>().HidePath(); // Сбрасываем путь

            context.Points = null;
            controller.Hide();
        }

    }

}
