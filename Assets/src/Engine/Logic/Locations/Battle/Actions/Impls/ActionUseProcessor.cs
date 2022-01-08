using Engine.Data;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор, выполняющий действие использование
    /// ---
    /// The processor that performs the action of using
    /// 
    /// </summary>
    public class ActionUseProcessor : BattleActionProcessor<BattleActionUseContext>
    {

        #region Properties

        /// <summary>
        /// Использование объекта на локации
        /// ---
        /// Using an object on location
        /// </summary>
        public override BattleAction Action => BattleAction.Use;

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
