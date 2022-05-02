using Engine.Data;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие использование
    /// Примером использование может быть - открытие или закрытие двери
    /// ---
    /// The processor that performs the action of using
    /// An example of a use would be to open or close a door
    /// 
    /// </summary>
    public class ActionUseProcessor : BattleActionProcessor<BattleActionUseContext>
    {

        #region Properties

        /// <summary>
        ///     Использование объекта на локации
        ///     ---
        ///     Using an object on location
        /// </summary>
        public override CharacterBattleAction Action => CharacterBattleAction.Use;

        #endregion

        public override void DoProcessAction(BattleActionUseContext context)
        {
            var controller = Controller;

            Game.Instance.Runtime.BattleContext.CurrentCharacterAP -= controller.NeedAP; // Тратим ОД

            var useObject = context.UseItem.GetComponent<IUseObjectController>();
            if (useObject != null)
            {
                useObject.DoUse();

                context.UseItem = null;
                controller.Hide();
            }
        }

        public override void DoRollbackAction(BattleActionUseContext context)
        {
            var controller = Controller;

            context.UseItem = null;
            controller.Hide();
        }

    }

}
