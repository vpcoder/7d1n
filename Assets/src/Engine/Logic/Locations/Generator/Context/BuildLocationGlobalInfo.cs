using System.Collections.Generic;
using Engine.Data.Generation.Elements;
using Engine.Logic.Locations.Generator.Markers;

namespace Engine.Logic.Locations.Generator
{
    
    public class BuildLocationGlobalInfo
    {

        public List<IMarker> Markers { get; set; }
        
        public System.Random BuildRandom { get; set; }
        
        /// <summary>
        ///     Текущая вариация стиля помещения/здания
        ///     ---
        ///     Current room/building style variation
        /// </summary>
        public BuildingElement BuildingElement { get; set; }

    }
    
}