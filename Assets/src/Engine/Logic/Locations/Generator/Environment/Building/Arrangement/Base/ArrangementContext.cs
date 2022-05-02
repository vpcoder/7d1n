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

        public IList<ArrangementItemContext<E>> GetItems(E type)
        {
            Items.TryGetValue(type, out var items);
            return items;
        }

    }

}
