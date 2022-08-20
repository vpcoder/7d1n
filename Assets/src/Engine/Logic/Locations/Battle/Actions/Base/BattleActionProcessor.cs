using System;
using UnityEngine;

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
    public abstract class BattleActionProcessor<T> : IBattleActionProcessor where T : IBattleActionContext
    {

        #region Properties

        /// <summary>
        ///     Действие которому соответствует процессор
        ///     ---
        ///     Action to which the processor corresponds
        /// </summary>
        public abstract CharacterBattleAction Action { get; }

        /// <summary>
        ///     Выполняет поиск контроллера действий в битве
        ///     ---
        ///     Searches for the action controller in the battle
        /// </summary>
        public BattleActionsController Controller
        {
            get
            {
                return ObjectFinder.Find<BattleActionsController>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Метод выполняет действие процессора в битве
        ///     ---
        ///     The method performs the action of the processor in the battle
        /// </summary>
        public void Process(IBattleActionContext context)
        {
            try
            {
                DoProcessAction((T)context);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        ///     Метод прекращает выполнять действие процессора
        ///     ---
        ///     The method stops executing the processor action
        /// </summary>
        public void Rollback(IBattleActionContext context)
        {
            try
            { 
                DoRollbackAction((T)context);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        ///     Метод выполняет действие процессора в битве
        ///     ---
        ///     The method performs the action of the processor in the battle
        /// </summary>
        /// <param name="context">
        ///     Контекст действия для текущего процессора
        ///     ---
        ///     Action context for the current processor
        /// </param>
        public abstract void DoProcessAction(T context);

        /// <summary>
        ///     Метод прекращает выполнять действие процессора
        ///     ---
        ///     The method stops executing the processor action
        /// </summary>
        /// <param name="context">
        ///     Контекст действия для текущего процессора
        ///     ---
        ///     Action context for the current processor
        /// </param>
        public abstract void DoRollbackAction(T context);

        #endregion

    }

}
