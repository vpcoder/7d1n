using Engine.Data.Generation.Elements;
using Engine.Logic.Locations.Generator.Environment.Building;

namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    /// 
    /// Контекст генератора помещений
    /// ---
    /// Context of the room generator
    /// 
    /// </summary>
    public class GenerationRoomContext : GenerationContextBase
    {
        
        /// <summary>
        ///     Информация о здании
        ///     ---
        ///     Building information
        /// </summary>
        public BuildLocationInfo BuildInfo { get; set; }

        /// <summary>
        ///     Информация по тайлам пола.
        ///     Полезна при расстановке мебели, стен и т.д.
        ///     ---
        ///     Information on the floor tiles.
        ///     Useful when arranging furniture, walls, etc.
        /// </summary>
        public TileCellInfo TilesInfo { get; set; }

        /// <summary>
        ///     Информация о сгенерированной мебели
        ///     ---
        ///     Information about the generated furniture (not including placement)
        /// </summary>
        public BuildFurnitureInfo FurnitureInfo { get; set; }

        /// <summary>
        ///     Тип помещения, которое сейчас генерируется
        ///     ---
        ///     The type of room that is currently being generated
        /// </summary>
        public RoomKindType RoomKindType { get; set; }

        /// <summary>
        ///     Набор случайных значений для текущего здания, этажа и комнаты (в совокупности)
        ///     ---
        ///     Set of random values for the current building, floor and room (in aggregate)
        /// </summary>
        public System.Random RoomRandom { get; set; }
        public System.Random BuildRandom { get; set; }
        public System.Random FloorRandom { get; set; }

    }
    
}
