using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building
{

    /// <summary>
    ///     Объект окружения для определённой комнаты (например для кухни, зала, спальни или туалета)
    ///     ---
    ///     Environment object for a specific room (e.g. kitchen, hall, bedroom, or toilet)
    /// </summary>
    /// <typeparam name="E">
    ///     Енум текущего объекта
    ///     ---
    ///     Enum of the current object
    /// </typeparam>
    public class EnvironmentItem<E> : IEnvironmentItem<E>
                                     where E : struct
    {
        /// <summary>
        ///     Текущий объект
        ///     ---
        ///     Current object
        /// </summary>
        public E Type { get; set; }

        /// <summary>
        ///     Ссылка на объект
        ///     ---
        ///     Item Reference
        /// </summary>
        public GameObject ToObject { get; set; }

        /// <summary>
        ///     Размеры объекта
        ///     ---
        ///     The size of the object
        /// </summary>
        public BoxCollider Bounds {
            get {
                return ToObject.GetComponent<BoxCollider>();
            }
        }

    }

}
