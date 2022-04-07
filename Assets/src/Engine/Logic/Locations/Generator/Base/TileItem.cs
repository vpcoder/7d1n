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

        public EdgeType LeftEdge { get; set; } = EdgeType.Empty;
        public EdgeType RightEdge { get; set; } = EdgeType.Empty;
        public EdgeType TopEdge { get; set; } = EdgeType.Empty;
        public EdgeType BottomEdge { get; set; } = EdgeType.Empty;

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
                return LeftEdge == EdgeType.Window ||
                       RightEdge == EdgeType.Window ||
                       TopEdge == EdgeType.Window ||
                       BottomEdge == EdgeType.Window;
            }
        }
        
        public bool HasDoor
        {
            get
            {
                return LeftEdge == EdgeType.Door ||
                       RightEdge == EdgeType.Door ||
                       TopEdge == EdgeType.Door ||
                       BottomEdge == EdgeType.Door;
            }
        }
        
        public bool HasWall
        {
            get
            {
                return LeftEdge == EdgeType.Wall ||
                       RightEdge == EdgeType.Wall ||
                       TopEdge == EdgeType.Wall ||
                       BottomEdge == EdgeType.Wall;
            }
        }
        
        #endregion
        
        #region Shared Methods

        /// <summary>
        ///     Получает все свободные сегменты на полу у стены
        ///     ---
        ///     Gets all free segments on the floor at the wall
        /// </summary>
        /// <returns>
        ///     
        /// </returns>
        public IList<TileSegmentType> GetEmptySegmentsOnTheFloorCloseToWall()
        {
            var list = new List<TileSegmentType>();
            var data = GetFurnitureOnTheFloorCloseToWall();
            if (data.Count == 0)
                return list;
            foreach (var entry in data)
            {
                if(entry.Value == null)
                    list.Add(entry.Key);
            }
            return list;
        }
        
        /// <summary>
        ///     Получает все сегменты на полу у стены (и пустые и заполненные)
        ///     ---
        ///     Gets all segments on the floor at the wall (both empty and filled)
        /// </summary>
        /// <returns>
        ///     
        /// </returns>
        public IDictionary<TileSegmentType, IEnvironmentItem> GetFurnitureOnTheFloorCloseToWall()
        {
            var result = new Dictionary<TileSegmentType, IEnvironmentItem>();
            if (!HasWall)
                return result;

            FurnitureData.TryGetValue(TileLayoutType.Floor, out var data);
            
            if (data == null)
                return result;
            
            var s00 = false;
            var s01 = false;
            var s10 = false;
            var s11 = false;
            
            if (LeftEdge == EdgeType.Wall)
            {
                data.TryGetValue(TileSegmentType.S00, out var s00Value);
                s00 = true;
                result.Add(TileSegmentType.S00, s00Value);
                
                data.TryGetValue(TileSegmentType.S10, out var s10Value);
                s10 = true;
                result.Add(TileSegmentType.S10, s10Value);
            }
            
            if (RightEdge == EdgeType.Wall)
            {
                data.TryGetValue(TileSegmentType.S01, out var s01Value);
                s01 = true;
                result.Add(TileSegmentType.S01, s01Value);
                
                data.TryGetValue(TileSegmentType.S11, out var s11Value);
                s11 = true;
                result.Add(TileSegmentType.S11, s11Value);
            }
            
            if (TopEdge == EdgeType.Wall)
            {
                if (!s00)
                {
                    data.TryGetValue(TileSegmentType.S00, out var s00Value);
                    result.Add(TileSegmentType.S00, s00Value);
                }
                if (!s01)
                {
                    data.TryGetValue(TileSegmentType.S01, out var s01Value);
                    result.Add(TileSegmentType.S01, s01Value);
                }
            }
            
            if (BottomEdge == EdgeType.Wall)
            {
                if (!s10)
                {
                    data.TryGetValue(TileSegmentType.S10, out var s10Value);
                    result.Add(TileSegmentType.S10, s10Value);
                }
                if (!s11)
                {
                    data.TryGetValue(TileSegmentType.S11, out var s11Value);
                    result.Add(TileSegmentType.S11, s11Value);
                }
            }
            
            return result;
        }
        
        public IDictionary<TileSegmentType, IEnvironmentItem> GetFurnitureOnTheCeiling()
        {
            FurnitureData.TryGetValue(TileLayoutType.Ceiling, out var data);
            return data;
        }
        
        public IDictionary<TileSegmentType, IEnvironmentItem> GetFurnitureOnTheFloor()
        {
            FurnitureData.TryGetValue(TileLayoutType.Floor, out var data);
            return data;
        }
        
        public IDictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>> GetFurnitureOnTheWall()
        {
            var result = new Dictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>>();
            if (!HasWall)
                return result;
            if (LeftEdge == EdgeType.Wall)
                ReadEdgeData(result, TileLayoutType.WallLeft);
            if (RightEdge == EdgeType.Wall)
                ReadEdgeData(result, TileLayoutType.WallRight);
            if (TopEdge == EdgeType.Wall)
                ReadEdgeData(result, TileLayoutType.WallTop);
            if (BottomEdge == EdgeType.Wall)
                ReadEdgeData(result, TileLayoutType.WallBottom);
            return result;
        }
        
        #endregion
        
        #region Utils Methods
        
        private void ReadEdgeData(IDictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>> data, TileLayoutType layout)
        {
            FurnitureData.TryGetValue(layout, out var edgeData);
            if (edgeData != null)
                data.Add(layout, edgeData);
        }
        
        #endregion

    }

}