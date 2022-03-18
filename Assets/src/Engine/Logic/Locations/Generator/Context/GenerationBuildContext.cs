using System;
using System.Collections.Generic;
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


        public string GetFloorIdByRoomType()
        {
            switch(BuildInfo.RoomType)
            {
                case RoomType.Empty:
                    return "";
                default:
                    throw new NotSupportedException();
            }
        }

    }

}
