using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var controller = Controller;

            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД

            var character = ObjectFinder.Find<LocationCharacter>();
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
