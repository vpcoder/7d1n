using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building
{
    
    public class RoomSuperFactory
    {
        
        #region Singleton

        private static readonly Lazy<RoomSuperFactory> instance = new Lazy<RoomSuperFactory>(() => new RoomSuperFactory());
        public static RoomSuperFactory Instance { get { return instance.Value; } }

        private RoomSuperFactory()
        {
            foreach (var factory in AssembliesHandler.CreateImplementations<IRoomFactory>())
                data.Add(factory.RoomType, factory);
        }

        #endregion

        /// <summary>
        ///     Коллекция всех фабрик для заданных комнат
        ///     ---
        ///     Collection of all factories for given rooms
        /// </summary>
        private IDictionary<RoomKindType, IRoomFactory> data = new Dictionary<RoomKindType, IRoomFactory>();

        /// <summary>
        ///     Находит фабрику мебели для комнаты по типу помещения
        ///     ---
        ///     Finding a furniture factory for a room by room type
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения
        ///     ---
        ///     Room type
        /// </param>
        /// <returns>
        ///     Фабрику мебели для заданного помещения
        ///     ---
        ///     Furniture factory for a given room
        /// </returns>
        public IRoomFactory Get(RoomKindType roomType)
        {
            data.TryGetValue(roomType, out var factory);
            return factory;
;        }

        /// <summary>
        ///     Находит фабрику мебели для комнаты по типу помещения
        ///     ---
        ///     Finding a furniture factory for a room by room type
        /// </summary>
        /// <param name="roomType">
        ///     Тип помещения
        ///     ---
        ///     Room type
        /// </param>
        /// <returns>
        ///     Фабрику мебели для заданного помещения
        ///     ---
        ///     Furniture factory for a given room
        /// </returns>
        public IRoomFactory<E> Get<E>(RoomKindType roomType) where E : struct
        {
            return (IRoomFactory<E>)Get(roomType);
        }
        
    }
    
}