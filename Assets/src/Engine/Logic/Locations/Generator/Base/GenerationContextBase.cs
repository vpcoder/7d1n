using System;
using System.Collections;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Markers;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Контекст любого генератора (генератора зданий, генератора открытых местностей, и прочих)
    /// ---
    /// Context of any generator (building generator, open terrain generator, and others)
    /// 
    /// </summary>
    public abstract class GenerationContextBase
    {
        
        /// <summary>
        ///     Все маркеры в комнате
        ///     ---
        ///     All markers in the room
        /// </summary>
        public ICollection<IMarker> AllMarkersInRoom { get; set; }

        /// <summary>
        ///     Все маркеры в комнате, представленные словарём, группированном по типу
        ///     ---
        ///     All markers in the room represented by the dictionary grouped by type
        /// </summary>
        public IDictionary<Type, IList<IMarker>> MarkersByTypeOnRoom { get; set; }

    }

}
