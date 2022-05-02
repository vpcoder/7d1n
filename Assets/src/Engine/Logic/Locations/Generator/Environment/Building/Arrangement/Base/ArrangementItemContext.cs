using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    
    public class ArrangementItemContext<E> where E : struct
    {

        /// <summary>
        ///     Месторасположение на сетке
        ///     ---
        ///     Location on the grid
        /// </summary>
        public TileSegmentLink Context { get; set; }

        /// <summary>
        ///     Созданный объект unity
        ///     ---
        ///     Created unity object
        /// </summary>
        public GameObject ToObject { get; set; }

        /// <summary>
        ///     Размещаемый ранее объект
        ///     ---
        ///     Previously placed object
        /// </summary>
        public IEnvironmentItem<E> Item { get; set; }
        
    }
    
}