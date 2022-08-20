using System.Collections.Generic;
using System.Linq;

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
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IList<TileItem> TilesData { get; set; }
        
        public IList<TileItem> TilesNearWall
        {
            get
            {
                return TilesData.Where(tile => tile.HasWall).ToList();
            }
        }
        
        public IList<TileItem> TilesNearWindow
        {
            get
            {
                return TilesData.Where(tile => tile.HasWindow).ToList();
            }
        }
        
        public IList<TileItem> TilesNearDoor
        {
            get
            {
                return TilesData.Where(tile => tile.HasDoor).ToList();
            }
        }
        
        #endregion

    }
    
}