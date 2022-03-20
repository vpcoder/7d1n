using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Фабрика итераторов для ИИ существ
    /// ---
    /// Iterator Factory for AI creatures
    /// 
    /// </summary>
    public class AiIteratorFactory
    {

        #region Singleton

        private static readonly Lazy<AiIteratorFactory> instance = new Lazy<AiIteratorFactory>(() => new AiIteratorFactory());
        public static AiIteratorFactory Instance { get { return instance.Value; } }
        private AiIteratorFactory()
        {
            foreach(var action in AssembliesHandler.CreateImplementations<IAiIterationAction>())
            {
                actions.Add(action.ActionType, action);
            }
        }

        #endregion

        /// <summary>
        ///     Итераторы действий ИИ существ сгруппированные по типу действия
        ///     ---
        ///     Iterators of AI creature actions grouped by type of action
        /// </summary>
        private IDictionary<NpcActionType, IAiIterationAction> actions = new Dictionary<NpcActionType, IAiIterationAction>();

        /// <summary>
        ///     Выполняет поиск итератора действий ИИ существа по типу действия
        ///     ---
        ///     Searches for a creature AI action iterator by action type
        /// </summary>
        /// <param name="type">
        ///     Тип действия, которое необходимо совершить существу
        ///     ---
        ///     The type of action to be performed by the creature
        /// </param>
        /// <returns>
        ///     Возвращает итератор действия.
        ///     ---
        ///     Returns the action iterator.
        /// </returns>
        public IAiIterationAction GetIterationAction(NpcActionType type)
        {
            return actions[type];
        }

    }

}
