using System;
using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Environment.Building
{

    /// <summary>
    /// 
    /// Фабрика для предметов конкретной комнаты
    /// ---
    /// Factory for specific room items
    /// 
    /// </summary>
    public abstract class RoomFactoryBase<E, TLoader> : IRoomFactory<E>
                                                        where E : struct
                                                        where TLoader : IEnvironmentItemLoader<E>
    {

        #region Hidden Fields

        /// <summary>
        ///     Все объекты в комнате, по типу объекта
        ///     ---
        ///     All objects in the room, by object type
        /// </summary>
        private IDictionary<E, IEnvironmentItem<E>> data;

        #endregion
        
        #region Properties

        /// <summary>
        ///     Комната в которой располагаются объекты
        ///     ---
        ///     The room in which the objects are located
        /// </summary>
        public abstract RoomKindType RoomType { get; }

        #endregion

        #region Ctor

        public RoomFactoryBase()
        {
            var loader = Activator.CreateInstance<TLoader>();
            this.data = ConvertToDictionary(loader.LoadAll());
        }

        #endregion

        #region Hidden Methods

        /// <summary>
        ///     Выполняет формирование словаря Объект -> Данные объекта
        ///     ---
        ///     Performs dictionary generation Object -> Object data
        /// </summary>
        /// <param name="collection">
        ///     Коллекция объектов, из которой будет сформирован словарь
        ///     ---
        ///     The collection of objects from which the dictionary will be formed
        /// </param>
        /// <returns>
        ///     Словарь: Объект -> Данные объекта
        ///     ---
        ///     Dictionary: Object -> Object data
        /// </returns>
        private IDictionary<E, IEnvironmentItem<E>> ConvertToDictionary(ICollection<IEnvironmentItem<E>> collection)
        {
            var dictionary = new Dictionary<E, IEnvironmentItem<E>>();
            foreach (var item in collection)
                dictionary.Add(item.Type, item);
            return dictionary;
        }

        #endregion

        #region Shared Methods

        /// <summary>
        ///     Получает все известные объекты для текущего типа комнаты
        ///     ---
        ///     Gets all known objects for the current room type
        /// </summary>
        /// <returns>
        ///     Возвращает хранимую коллекцию объектов
        ///     ---
        ///     Returns the stored collection of objects
        /// </returns>
        public ICollection<IEnvironmentItem<E>> GetAllItems()
        {
            return data.Values;
        }

        /// <summary>
        ///     Находит конкретный объект по его типу
        ///     ---
        ///     Finds a specific object by its type
        /// </summary>
        /// <param name="type">
        ///     Тип искомого объекта
        ///     ---
        ///     The type of object you are looking for
        /// </param>
        /// <returns>
        ///     Найденный объект
        ///     ---
        ///     Found object
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        ///     Объекта не существует
        ///     ---
        ///     The object does not exist
        /// </exception>
        /// 
        public IEnvironmentItem<E> Get(E type)
        {
            if (!data.TryGetValue(type, out var item))
                throw new KeyNotFoundException("object link by type '" + type.ToString() + "' not founded!");
            return item;
        }

        #endregion

    }

}
