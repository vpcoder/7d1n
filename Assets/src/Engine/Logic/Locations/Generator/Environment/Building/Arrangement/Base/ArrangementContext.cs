using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{

    /// <summary>
    /// 
    /// Контекст размещения объектов в сцене
    /// ---
    /// Context of the placement of objects in the scene
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Енум со списком объектов в помещении
    ///     ---
    ///     Enum with the list of objects in the room
    /// </typeparam>
    public class ArrangementContext<E> where E : struct
    {

        /// <summary>
        ///     Список уже размещённых объектов в сцене
        ///     ---
        ///     List of already placed objects in the scene
        /// </summary>
        public IDictionary<E, IList<ArrangementItemContext<E>>> Items { get; } = new Dictionary<E, IList<ArrangementItemContext<E>>>();

        /// <summary>
        ///     Список всех объектов, который нам сказали разместить в сцене
        ///     ---
        ///     The list of all the objects we were told to place in the scene
        /// </summary>
        public ICollection<IEnvironmentItem<E>> AvailableItems { get; set; }

        /// <summary>
        ///     Список оставшихся объектов, которые мы планируем разместить
        ///     ---
        ///     A list of the remaining facilities that we plan to place
        /// </summary>
        public ICollection<IEnvironmentItem<E>> RemainingItems { get; set; }

        /// <summary>
        ///     Выполняет поиск среди уже расставленных предметов по типу
        ///     ---
        ///     Searches among already placed items by type
        /// </summary>
        /// <param name="type">
        ///     Тип искомого предмета
        ///     ---
        ///     The type of item you are looking for
        /// </param>
        /// <returns>
        ///     Возвращает коллекцию найденных предметов по типу, которые были расставлены ранее
        ///     ---
        ///     Returns a collection of found items by type, which were arranged earlier
        /// </returns>
        public IList<ArrangementItemContext<E>> GetItems(E type)
        {
            Items.TryGetValue(type, out var items);
            return items;
        }
        
        /// <summary>
        ///     Выполняет поиск среди уже расставленных предметов по типам
        ///     ---
        ///     Searches among already placed items by type
        /// </summary>
        /// <param name="types">
        ///     Типы искомого предмета
        ///     ---
        ///     Types of item you are looking for
        /// </param>
        /// <returns>
        ///     Возвращает коллекцию найденных предметов по типам, которые были расставлены ранее
        ///     ---
        ///     Returns a collection of found items by type, which were arranged earlier
        /// </returns>
        public IList<ArrangementItemContext<E>> GetItems(params E[] types)
        {
            var list = new List<ArrangementItemContext<E>>();
            if (types == null || types.Length == 0)
                return list;
            foreach (var type in types)
                list.AddRange(GetItems(type));
            return list;
        }

    }

}
