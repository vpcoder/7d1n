using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using Mapbox.Map;

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
        
        
        private readonly IDictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>> furnitureData = new Dictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>>();

        
        #region Properties

        public FloorMarker Marker { get; set; }

        public TileItem LeftOfThis { get; set; }
        public TileItem RightOfThis { get; set; }
        public TileItem TopOfThis { get; set; }
        public TileItem BottomOfThis { get; set; }

        public EdgeType LeftEdge { get; set; } = EdgeType.Empty;
        public EdgeType RightEdge { get; set; } = EdgeType.Empty;
        public EdgeType TopEdge { get; set; } = EdgeType.Empty;
        public EdgeType BottomEdge { get; set; } = EdgeType.Empty;

        public EdgeType GetEdge(EdgeLayout layout)
        {
            switch (layout)
            {
                case EdgeLayout.LeftInside:
                case EdgeLayout.LeftOutside:
                    return LeftEdge;
                case EdgeLayout.RightInside:
                case EdgeLayout.RightOutside:
                    return RightEdge;
                case EdgeLayout.TopInside:
                case EdgeLayout.TopOutside:
                    return TopEdge;
                case EdgeLayout.BottomInside:
                case EdgeLayout.BottomOutside:
                    return BottomEdge;
                default:
                    throw new NotSupportedException(layout.ToString());
            }
        }
        
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

        public bool HasNotAnyEdge
        {
            get
            {
                return !HasAnyEdge;
            }
        }
        
        public bool HasAnyEdge
        {
            get
            {
                return HasWindow || HasDoor || HasWall;
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

        public void Set(TileLayoutType layout, TileSegmentType segment, IEnvironmentItem item)
        {
            furnitureData.TryGetValue(layout, out var data);
            if (data == null)
            {
                data = new Dictionary<TileSegmentType, IEnvironmentItem>();
                furnitureData[layout] = data;
            }
            data[segment] = item;
        }

        public IEnvironmentItem Get(TileLayoutType layout, TileSegmentType segment)
        {
            furnitureData.TryGetValue(layout, out var data);
            if (data == null)
                return null;
            return data[segment];
        }
        
        /// <summary>
        ///     Получает все свободные сегменты на полу у стены
        ///     ---
        ///     Gets all free segments on the floor at the wall
        /// </summary>
        /// <returns>
        ///     
        /// </returns>
        public IList<TileSegmentLink> GetEmptySegmentsOnTheFloorCloseToWall()
        {
            var list = new List<TileSegmentLink>();
            var data = GetFurnitureOnTheFloorCloseToWall();
            if (data.Count == 0)
                return list;
            foreach (var link in data)
            {
                if(link.Item == null)
                    list.Add(link);
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
        public IList<TileSegmentLink> GetFurnitureOnTheFloorCloseToWall()
        {
            return GetFurnitureOnTheFloorCloseToWall(EdgeType.Wall);
        }
        
        public IList<TileSegmentLink> GetFurnitureOnTheFloorCloseToWindow()
        {
            return GetFurnitureOnTheFloorCloseToWall(EdgeType.Window);
        }
        
        public IList<TileSegmentLink> GetFurnitureOnTheFloorCloseToDoor()
        {
            return GetFurnitureOnTheFloorCloseToWall(EdgeType.Door);
        }

        private IList<TileSegmentLink> GetFurnitureOnTheFloorCloseToWall(EdgeType edge)
        {
            var result = new List<TileSegmentLink>();
            if (HasNotAnyEdge)
                return result;

            var layout = TileLayoutType.Floor;

            furnitureData.TryGetValue(layout, out var data);

            if (data == null)
                data = new Dictionary<TileSegmentType, IEnvironmentItem>();
            
            var s00 = false;
            var s01 = false;
            var s10 = false;
            var s11 = false;
            
            if (LeftEdge == edge)
            {
                data.TryGetValue(TileSegmentType.S00, out var s00Value);
                s00 = true;
                result.Add(new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s00Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S00,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.LeftInside,
                });
                data.TryGetValue(TileSegmentType.S10, out var s10Value);
                s10 = true;
                result.Add(new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s10Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S10,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.LeftInside,
                });
            }
            
            if (RightEdge == edge)
            {
                data.TryGetValue(TileSegmentType.S01, out var s01Value);
                s01 = true;
                result.Add(new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s01Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S01,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.RightInside,
                });
                
                data.TryGetValue(TileSegmentType.S11, out var s11Value);
                s11 = true;
                result.Add(new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s11Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S11,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.RightInside,
                });
            }
            
            if (TopEdge == edge)
            {
                if (!s00)
                {
                    data.TryGetValue(TileSegmentType.S00, out var s00Value);
                    result.Add(new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s00Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S00,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.TopInside,
                    });
                }
                if (!s01)
                {
                    data.TryGetValue(TileSegmentType.S01, out var s01Value);
                    result.Add(new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s01Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S01,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.TopInside,
                    });
                }
            }
            
            if (BottomEdge == edge)
            {
                if (!s10)
                {
                    data.TryGetValue(TileSegmentType.S10, out var s10Value);
                    result.Add(new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s10Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S10,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.BottomInside,
                    });
                }
                if (!s11)
                {
                    data.TryGetValue(TileSegmentType.S11, out var s11Value);
                    result.Add(new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s11Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S11,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.BottomInside,
                    });
                }
            }
            
            return result;
        }
        
        public IDictionary<TileSegmentType, IEnvironmentItem> GetFurnitureOnTheCeiling()
        {
            furnitureData.TryGetValue(TileLayoutType.Ceiling, out var data);
            return data;
        }
        
        public IDictionary<TileSegmentType, IEnvironmentItem> GetFurnitureOnTheFloor()
        {
            furnitureData.TryGetValue(TileLayoutType.Floor, out var data);
            return data;
        }
        
        public IDictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>> GetFurnitureOnTheWall()
        {
            var result = new Dictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>>();
            if (HasNotAnyEdge)
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
            furnitureData.TryGetValue(layout, out var edgeData);
            if (edgeData != null)
                data.Add(layout, edgeData);
        }
        
        #endregion

    }

}