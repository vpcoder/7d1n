using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{

    /// <summary>
    /// 
    /// Информация о размещении объекта в помещении
    /// ---
    /// Information about the placement of the facility on the premises
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Енум со списком объектов в помещении
    ///     ---
    ///     Enum with the list of objects in the room
    /// </typeparam>
    public class ArrangementItem<E> where E : struct
    {

        /// <summary>
        ///     Размещаемый ранее объект
        ///     ---
        ///     Previously placed object
        /// </summary>
        public IEnvironmentItem<E> Item { get; set; }

        /// <summary>
        ///     Ссылка на созданный экземпляр объекта, который мы разместили в сцене
        ///     ---
        ///     Reference to the created instance of the object we placed in the scene
        /// </summary>
        public GameObject ToObject { get; set; }

    }

}
