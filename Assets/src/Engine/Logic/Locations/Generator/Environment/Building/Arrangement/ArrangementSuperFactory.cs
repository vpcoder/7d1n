using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{

    /// <summary>
    /// 
    /// Фабрика расстановки для жилых помещений
    /// ---
    /// The residential layout factory
    /// 
    /// </summary>
    public class ArrangementSuperFactory
    {

        #region Singleton

        private static Lazy<ArrangementSuperFactory> instance = new Lazy<ArrangementSuperFactory>(() => new ArrangementSuperFactory());

        public static ArrangementSuperFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ArrangementSuperFactory()
        {
            foreach (var processor in AssembliesHandler.CreateImplementations<IArrangementProcessor>())
                data.AddInToList(processor.RoomType, processor);
        }

        #endregion

        #region Hidden Fields

        /// <summary>
        ///     Коллекция всех расстановщиков для жилых помещений
        ///     ---
        ///     A collection of all residential formulators
        /// </summary>
        private IDictionary<RoomKindType, IList<IArrangementProcessor>> data = new Dictionary<RoomKindType, IList<IArrangementProcessor>>();

        #endregion

        #region Shared Fields

        /// <summary>
        ///     Выполняет поиск процессоров расстановки по типу жилого помещения
        ///     ---
        ///     Performs a search for layout processors by type of living quarters
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения, для которого нужно найти процессоры
        ///     ---
        ///     The type of room for which you need to find processors
        /// </param>
        /// <returns>
        ///     Список доступных процессоров расстановки.
        ///     null - если таких нет
        ///     ---
        ///     List of available formation processors
        ///     null - if there are none
        /// </returns>
        public IList<IArrangementProcessor> Get(RoomKindType roomType)
        {
            data.TryGetValue(roomType, out var list);
            return list;
        }

        /// <summary>
        ///     Получает конкретный процессор по типу помещения и по индексу процессора
        ///     ---
        ///     Gets a specific processor by room type and by processor index
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения, для которого нужно найти процессоры
        ///     ---
        ///     The type of room for which you need to find processors
        /// </param>
        /// <param name="index">
        ///     Индекс процессора в списке
        ///     ---
        ///     Processor index in the list
        /// </param>
        /// <returns>
        ///     Процессор расстановки, либо null, если такого не нашлось
        ///     ---
        ///     The placement processor, or null if no such processor is found
        /// </returns>
        public IArrangementProcessor Get(RoomKindType roomType, int index)
        {
            return Get(roomType)?[index];
        }

        /// <summary>
        ///     Считает количество доступных процессоров расстановки для заданного типа помещения
        ///     ---
        ///     Counts the number of available placement processors for a given room type
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения, для которого нужно найти процессоры
        ///     ---
        ///     The type of room for which you need to find processors
        /// </param>
        /// <returns>
        ///     Количество найденных процессоров расстановки для данного типа помещения
        ///     ---
        ///     Number of found arrangement processors for a given room type
        /// </returns>
        public int GetCount(RoomKindType roomType)
        {
            return Get(roomType)?.Count ?? 0;
        }

        #endregion

    }

}
