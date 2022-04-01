using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    /// 
    /// Информация по тайлам пола.
    /// Полезна при расстановке мебели, стен и т.д.
    /// ---
    /// Information on the floor tiles.
    /// Useful when arranging furniture, walls, etc.
    /// 
    /// </summary>
    public class TileCellInfo
    {

        public ICollection<TileItem> TilesData { get; set; }

    }
    
}