using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Furniture;
using UnityEngine;

namespace src.Engine.Logic.Locations.Generator.Furniture
{
    
    /// <summary>
    ///
    /// Фабрика генерации мебели для определённого помещения
    /// ---
    /// Furniture generation factory for a specific room
    /// 
    /// </summary>
    public class FurnitureProcessorSuperFactory
    {
        
        #region Singleton

        private static Lazy<FurnitureProcessorSuperFactory> instance = new Lazy<FurnitureProcessorSuperFactory>(() => new FurnitureProcessorSuperFactory());

        public static FurnitureProcessorSuperFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private FurnitureProcessorSuperFactory()
        {
            foreach (var processor in AssembliesHandler.CreateImplementations<IFurnitureProcessor>())
                data.AddInToList(processor.RoomType, processor);
        }

        #endregion

        #region Hidden Fields

        /// <summary>
        ///     Коллекция всех генераторов мебели
        ///     ---
        ///     A collection of all furniture generators
        /// </summary>
        private IDictionary<RoomKindType, IList<IFurnitureProcessor>> data = new Dictionary<RoomKindType, IList<IFurnitureProcessor>>();

        #endregion

        #region Shared Fields

        /// <summary>
        ///     Выполняет генерацию мебели по типу помещения
        ///     ---
        ///     Performs furniture generation by room type
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения, для которого нужно сгенерировать мебель
        ///     ---
        ///     Type of room for which you want to generate furniture
        /// </param>
        /// <returns>
        ///     Список доступных процессоров генерации мебели.
        ///     null - если таких нет
        ///     ---
        ///     List of available furniture generation processors.
        ///     null - if there are none
        /// </returns>
        public IList<IFurnitureProcessor> Get(RoomKindType roomType)
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
        ///     Процессор генерации мебели, либо null, если такого не нашлось
        ///     ---
        ///     Furniture generation processor, or null if no such processor was found
        /// </returns>
        public IFurnitureProcessor Get(RoomKindType roomType, int index)
        {
            return Get(roomType)?[index];
        }

        /// <summary>
        ///     Считает количество доступных процессоров генерации мебели для заданного типа помещения
        ///     ---
        ///     Counts the number of available furniture generation processors for a given room type
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения, для которого нужно найти процессоры
        ///     ---
        ///     The type of room for which you need to find processors
        /// </param>
        /// <returns>
        ///     Количество найденных процессоров генерации мебели для данного типа помещения
        ///     ---
        ///     Number of found furniture generation processors for a given room type
        /// </returns>
        public int GetCount(RoomKindType roomType)
        {
            return Get(roomType)?.Count ?? 0;
        }

        #endregion
        
    }
}