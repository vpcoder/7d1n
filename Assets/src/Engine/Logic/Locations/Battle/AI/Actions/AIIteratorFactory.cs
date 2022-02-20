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
    public class AIIteratorFactory
    {

        #region Singleton

        private static readonly Lazy<AIIteratorFactory> instance = new Lazy<AIIteratorFactory>(() => new AIIteratorFactory());
        public static AIIteratorFactory Instance { get { return instance.Value; } }
        private AIIteratorFactory()
        {
            foreach(var action in AssembliesHandler.CreateImplementations<IAIIterationAction>())
            {
                actions.Add(action.ActionType, action);
            }
        }

        #endregion

        /// <summary>
        /// Итераторы действий ИИ существ сгруппированные по типу действия
        /// ---
        /// 
        /// </summary>
        private IDictionary<NpcActionType, IAIIterationAction> actions = new Dictionary<NpcActionType, IAIIterationAction>();

        /// <summary>
        /// Выполняет поиск итератора действий ИИ существа по типу действия
        /// ---
        /// 
        /// </summary>
        /// <param name="type">
        /// Тип действия, которое необходимо совершить существу
        /// ---
        /// 
        /// </param>
        /// <returns>
        /// Возвращает итератор действия.
        /// </returns>
        public IAIIterationAction GetIterationAction(NpcActionType type)
        {
            return actions[type];
        }

    }

}
