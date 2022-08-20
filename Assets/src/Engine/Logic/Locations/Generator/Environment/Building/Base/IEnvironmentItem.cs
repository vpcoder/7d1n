using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building
{

    /// <summary>
    /// 
    /// Объект окружения
    /// ---
    /// Environment object
    /// 
    /// </summary>
    public interface IEnvironmentItem
    {

        /// <summary>
        ///     Ссылка на объект
        ///     ---
        ///     Item Reference
        /// </summary>
        GameObject ToObject { get; }

        /// <summary>
        ///     Размеры объекта
        ///     ---
        ///     The size of the object
        /// </summary>
        BoxCollider Bounds { get; }

    }

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
    public interface IEnvironmentItem<E> : IEnvironmentItem
                                 where E : struct
    {

        /// <summary>
        ///     Текущий объект
        ///     ---
        ///     Current object
        /// </summary>
        E Type { get; }

    }

}
