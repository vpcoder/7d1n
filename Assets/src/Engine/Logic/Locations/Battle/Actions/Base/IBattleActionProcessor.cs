
namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Процессор выполняющий обработку действия в битве.
    /// Примером действия может служить - перемещение, атака, перезарядка, использование предмета из инвентаря и т.д.
    /// ---
    /// The processor that processes the action in the battle.
    /// An example of an action is moving, attacking, reloading, using an item from the inventory, etc.
    /// 
    /// </summary>
    public interface IBattleActionProcessor
    {

        #region Properties

        /// <summary>
        ///     Действие которому соответствует процессор
        ///     ---
        ///     Action to which the processor corresponds
        /// </summary>
        CharacterBattleAction Action { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Метод выполняет действие процессора в битве
        ///     ---
        ///     The method performs the action of the processor in the battle
        /// </summary>
        void Process(IBattleActionContext context);

        /// <summary>
        ///     Метод прекращает выполнять действие процессора
        ///     ---
        ///     The method stops executing the processor action
        /// </summary>
        void Rollback(IBattleActionContext context);

        #endregion

    }

}
