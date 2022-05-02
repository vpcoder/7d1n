using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    /// 
    /// Фабрика для поиска процессоров по действиям
    /// ---
    /// Factory for searching processors by action
    /// 
    /// </summary>
    public class CharacterBattleActionFactory
    {

        #region Singleton

        private static Lazy<CharacterBattleActionFactory> instance = new Lazy<CharacterBattleActionFactory>(() => new CharacterBattleActionFactory());

        public static CharacterBattleActionFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private CharacterBattleActionFactory()
        {
            LoadFactory();
        }

        #endregion

        /// <summary>
        ///     Кэш в виде словаря Действие -> Процессор
        ///     ---
        ///     Cache as a dictionary Action -> Processor
        /// </summary>
        private readonly IDictionary<CharacterBattleAction, IBattleActionProcessor> data = new Dictionary<CharacterBattleAction, IBattleActionProcessor>();

        /// <summary>
        ///     Выполняет поиск всех реализаций для IBattleActionProcessor, и запись их в data
        ///     ---
        ///     Searches for all IBattleActionProcessor implementations, and writes them into the data
        /// </summary>
        private void LoadFactory()
        {
            data.Clear();

            foreach(var processor in AssembliesHandler.CreateImplementations<IBattleActionProcessor>())
                data.Add(processor.Action, processor);
        }

        /// <summary>
        ///     Выполняет извлечение процессора по действию
        ///     ---
        ///     Performs processor extraction by action
        /// </summary>
        /// <param name="action">
        ///     Действие для которого нужно найти процессор
        ///     ---
        ///     Action for which you need to find the processor
        /// </param>
        /// <returns>
        ///     Возвращает найденный процессор
        ///     ---
        ///     Returns the found processor
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        ///     Не удалось найти процессор для указанного действия
        ///     ---
        ///     Failed to find a processor for the specified action
        /// </exception>
        public IBattleActionProcessor TryGetProcessor(CharacterBattleAction action)
        {
            if(data.TryGetValue(action, out var processor))
                return processor;

#if UNITY_EDITOR && DEBUG
            Debug.LogError("key '" + action.ToString() + "', data size " + data.Count.ToString());
#endif

            throw new KeyNotFoundException("key '" + action.ToString() + "' not founded!");
        }

        /// <summary>
        ///     Выполняет поиск процессора и вызов метода обработки
        ///     ---
        ///     Searches for the processor and calls the process method
        /// </summary>
        /// <param name="action">
        ///     Действие для которого нужно найти процессор
        ///     ---
        ///     Action for which you need to find the processor
        /// </param>
        /// <param name="context">
        ///     Контекст для процессора
        ///     ---
        ///     Context for the processor
        /// </param>
        /// <exception cref="KeyNotFoundException">
        ///     Не удалось найти процессор для указанного действия
        ///     ---
        ///     Failed to find a processor for the specified action
        /// </exception>
        public void InvokeProcess(CharacterBattleAction action, IBattleActionContext context)
        {
            var processor = TryGetProcessor(action);
            processor.Process(context);
        }

        /// <summary>
        ///     Выполняет поиск процессора и вызов метода обработки
        ///     ---
        ///     Searches for the processor and calls the rollback method
        /// </summary>
        /// <param name="action">
        ///     Действие для которого нужно найти процессор
        ///     ---
        ///     Action for which you need to find the processor
        /// </param>
        /// <param name="context">
        ///     Контекст для процессора
        ///     ---
        ///     Context for the processor
        /// </param>
        /// <exception cref="KeyNotFoundException">
        ///     Не удалось найти процессор для указанного действия
        ///     ---
        ///     Failed to find a processor for the specified action
        /// </exception>
        public void InvokeRollback(CharacterBattleAction action, IBattleActionContext context)
        {
            var processor = TryGetProcessor(action);
            processor.Rollback(context);
        }

    }

}
