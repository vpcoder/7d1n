using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;

namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    /// 
    /// Тайл пола.
    /// Один тайл пола делится на 4 более маленьких сегмента, в каждый маленький сегмент можно поставить один элемент мебели
    /// ---
    /// Floor Tile.
    /// One floor tile is divided into 4 smaller segments, each small segment can be equipped with one piece of furniture
    ///
    /// TileItem
    /// -----------
    /// |  1 |  2 | <- TileSegments
    /// |----|----|
    /// |  3 |  4 |
    /// -----------
    /// 
    ///             --------
    ///             |      | <- TopOfThis
    /// LeftOfThis  |      | 
    ///       |     |      |
    ///       v     --------
    ///     ------------------------
    ///     |      ||      ||      |
    ///     |      || this ||      | <- RightOfThis
    ///     |      ||      ||      |
    ///     ------------------------
    ///             --------
    ///             |      |
    ///             |      | 
    ///             |      | <- BottomOfThis
    ///             --------
    /// 
    /// </summary>
    public class TileItem
    {
        
        #region Properties
        
        public IDictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>> FurnitureData { get; set; }
        
        public FloorMarker Marker { get; set; }

        public TileItem LeftOfThis { get; set; }
        public TileItem RightOfThis { get; set; }
        public TileItem TopOfThis { get; set; }
        public TileItem BottomOfThis { get; set; }

        public EdgeItem LeftEdge { get; set; } = EdgeItem.Empty;
        public EdgeItem RightEdge { get; set; } = EdgeItem.Empty;
        public EdgeItem TopEdge { get; set; } = EdgeItem.Empty;
        public EdgeItem BottomEdge { get; set; } = EdgeItem.Empty;

        public bool IsSurrounded
        {
            get
            {
                return !IsNotSurrounded;
            }
        }
        
        public bool IsNotSurrounded
        {
            get
            {
                return LeftOfThis == null || RightOfThis == null || TopOfThis != null || BottomOfThis != null;
            }
        }
        
        public bool HasWindow
        {
            get
            {
                return LeftEdge.EdgeType == EdgeType.Window ||
                       RightEdge.EdgeType == EdgeType.Window ||
                       TopEdge.EdgeType == EdgeType.Window ||
                       BottomEdge.EdgeType == EdgeType.Window;
            }
        }
        
        public bool HasDoor
        {
            get
            {
                return LeftEdge.EdgeType == EdgeType.Door ||
                       RightEdge.EdgeType == EdgeType.Door ||
                       TopEdge.EdgeType == EdgeType.Door ||
                       BottomEdge.EdgeType == EdgeType.Door;
            }
        }
        
        public bool HasWall
        {
            get
            {
                return LeftEdge.EdgeType == EdgeType.Wall ||
                       RightEdge.EdgeType == EdgeType.Wall ||
                       TopEdge.EdgeType == EdgeType.Wall ||
                       BottomEdge.EdgeType == EdgeType.Wall;
            }
        }
        
        #endregion
        
        public IDictionary<TileSegmentType, IEnvironmentItem> GetFurniture(TileLayoutType layoutType)
        {
            FurnitureData.TryGetValue(layoutType, out var data);
            return data;
        }
        
    }

}