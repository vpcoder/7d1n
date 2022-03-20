using System;
using System.Collections.Generic;
using Engine.Data.Generation.Elements;
using Engine.Logic.Locations.Generator.Markers;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Контекст генератора помещений
    /// ---
    /// Context of the room generator
    /// 
    /// </summary>
    public class GenerationBuildContext : GenerationContextBase
    {

        /// <summary>
        ///     Информация о здании
        ///     ---
        ///     Building information
        /// </summary>
        public BuildLocationInfo BuildInfo { get; set; }

        /// <summary>
        ///     Текущая вариация стиля помещения/здания
        ///     ---
        ///     Current room/building style variation
        /// </summary>
        public BuildingElement BuildingElement { get; set; }

    }

}
