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
        ///     All markers in the scene represented by the enumeration
        ///     ---
        ///     All markers in the scene
        /// </summary>
        public ICollection<IMarker> AllMarkers { get; set; }

        /// <summary>
        ///     Все маркеры в сцене, представленные словарём, группированном по типу
        ///     ---
        ///     All markers in the scene represented by the dictionary grouped by type
        /// </summary>
        public IDictionary<Type, IList<IMarker>> MarkersByType { get; set; }

    }

}
