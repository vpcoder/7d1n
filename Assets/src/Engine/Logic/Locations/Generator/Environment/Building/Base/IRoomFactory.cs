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
    public interface IRoomFactory
    {
        
        /// <summary>
        ///     Комната в которой находятся предметы
        ///     ---
        ///     The room in which the objects are located
        /// </summary>
        RoomKindType RoomType { get; }
        
    }
    
    /// <summary>
    /// 
    /// Фабрика для предметов конкретной комнаты
    /// ---
    /// Factory for specific room items
    /// 
    /// </summary>
    public interface IRoomFactory<E> : IRoomFactory
                                    where E : struct
    {

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
        ICollection<IEnvironmentItem<E>> GetAllItems();

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
        IEnvironmentItem<E> Get(E type);

    }

}
