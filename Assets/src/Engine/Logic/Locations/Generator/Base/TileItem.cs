using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

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
    /// |  1 |  3 | <- TileSegments
    /// |----|----|
    /// |  2 |  4 |
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

        /// <summary>
        ///     Карта тайловой сетки для текущего тайла
        ///     ---
        ///     Tile grid map for the current tile
        /// </summary>
        private readonly IDictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>> furnitureData = new Dictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>>();

        #region Properties

        /// <summary>
        ///     Ссылка на маркер, по которому создан тайл
        ///     ---
        ///     Link to the marker that created the tile
        /// </summary>
        public FloorMarker Marker { get; set; }

        public TileItem LeftOfThis { get; set; }
        public TileItem RightOfThis { get; set; }
        public TileItem TopOfThis { get; set; }
        public TileItem BottomOfThis { get; set; }

        public EdgeType LeftEdge { get; set; } = EdgeType.Empty;
        public EdgeType RightEdge { get; set; } = EdgeType.Empty;
        public EdgeType TopEdge { get; set; } = EdgeType.Empty;
        public EdgeType BottomEdge { get; set; } = EdgeType.Empty;

        public TileItem GetTile(TileLayoutType tileLayout)
        {
            switch (tileLayout)
            {
                case TileLayoutType.WallBottom:
                    return BottomOfThis;
                case TileLayoutType.WallLeft:
                    return LeftOfThis;
                case TileLayoutType.WallRight:
                    return RightOfThis;
                case TileLayoutType.WallTop:
                    return TopOfThis;
                default:
                    throw new NotSupportedException();
            }
        }
        
        public TileItem GetTile(EdgeLayout layout)
        {
            switch (layout)
            {
                case EdgeLayout.LeftInside:
                case EdgeLayout.LeftOutside:
                    return LeftOfThis;
                case EdgeLayout.RightInside:
                case EdgeLayout.RightOutside:
                    return RightOfThis;
                case EdgeLayout.TopInside:
                case EdgeLayout.TopOutside:
                    return TopOfThis;
                case EdgeLayout.BottomInside:
                case EdgeLayout.BottomOutside:
                    return BottomOfThis;
                default:
                    throw new NotSupportedException(layout.ToString());
            }
        } 
        
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

        /// <summary>
        ///     По краям этого тайла есть другие тайлы пола, со всех сторон
        ///     ---
        ///     There are other floor tiles on the edges of this tile, on all sides
        /// </summary>
        public bool IsSurrounded
        {
            get
            {
                return !IsNotSurrounded;
            }
        }
        
        /// <summary>
        ///     Тайл не окружён другими тайлами со всех сторон
        ///     ---
        ///     Tile is not surrounded by other tiles on all sides
        /// </summary>
        public bool IsNotSurrounded
        {
            get
            {
                return LeftOfThis == null || RightOfThis == null || TopOfThis != null || BottomOfThis != null;
            }
        }

        /// <summary>
        ///     На тайле есть внутренний угол, образованный как минимум двумя стенами
        ///     ---
        ///     There is an inner corner formed by at least two walls on the timeline
        /// </summary>
        public bool HasInnerCorner
        {
            get
            {
                return (LeftEdge != EdgeType.Empty || RightEdge != EdgeType.Empty)
                       && (TopEdge != EdgeType.Empty || BottomEdge != EdgeType.Empty);
            }
        }

        /// <summary>
        ///     Определяет что сегмент находится во внутреннем углу
        ///     ---
        ///     
        /// </summary>
        /// <param name="segment">
        ///     Искомый сегмент
        ///     ---
        ///     
        /// </param>
        /// <returns>
        ///     true - если сегмент находится во внутреннем углу
        ///     false - если внутреннего угла нет, или сегмент находится не в нём
        ///     ---
        ///     
        /// </returns>
        public bool InCorner(TileSegmentType segment)
        {
            switch (segment)
            {
                case TileSegmentType.S00:
                    return LeftEdge != EdgeType.Empty && BottomEdge != EdgeType.Empty;
                case TileSegmentType.S01:
                    return LeftEdge != EdgeType.Empty && TopEdge != EdgeType.Empty;
                case TileSegmentType.S10:
                    return RightEdge != EdgeType.Empty && BottomEdge != EdgeType.Empty;
                case TileSegmentType.S11:
                    return RightEdge != EdgeType.Empty && TopEdge != EdgeType.Empty;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///     За тайлом есть внешний угол, образованный стенами двух соседних тайлов
        ///     ---
        ///     There is an outer corner formed by the walls of the two neighboring tiles
        /// </summary>
        public bool HasOuterCorner
        {
            get
            {
                // Левый-нижний
                if (HasNotEmptyEdge(LeftOfThis, EdgeLayout.BottomInside) &&
                    HasNotEmptyEdge(BottomOfThis, EdgeLayout.LeftInside))
                {
                    return true;
                }
                // Левый-верхний
                if (HasNotEmptyEdge(LeftOfThis, EdgeLayout.TopInside) &&
                    HasNotEmptyEdge(TopOfThis, EdgeLayout.LeftInside))
                {
                    return true;
                }
                // Правый-нижний
                if (HasNotEmptyEdge(RightOfThis, EdgeLayout.BottomInside) &&
                    HasNotEmptyEdge(BottomOfThis, EdgeLayout.RightInside))
                {
                    return true;
                }
                // Правый-верхний
                if (HasNotEmptyEdge(RightOfThis, EdgeLayout.TopInside) &&
                    HasNotEmptyEdge(TopOfThis, EdgeLayout.RightInside))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        ///     На тайле в указанном слое есть стены?
        ///     ---
        ///     Is there a wall in the specified layer on the tile?
        /// </summary>
        /// <param name="tile">
        ///     Тайл на который смотрим
        ///     ---
        ///     Tile we are looking at
        /// </param>
        /// <param name="layout">
        ///     Слой на котором ищем стену
        ///     ---
        ///     The layer on which we are looking for a wall
        /// </param>
        /// <returns>
        ///     true - если на указанном тайле в указанной стороне есть какая либо стена
        ///     false - если на указанном тайле в указанной стороне ничего нет
        ///     ---
        ///     true - if on the specified side of the specified tile there is a wall
        ///     false - if there is nothing on the specified side on the specified side
        /// </returns>
        private bool HasNotEmptyEdge(TileItem tile, EdgeLayout layout)
        {
            if (tile == null)
                return false;
            return tile.GetEdge(layout) != EdgeType.Empty;
        }

        /// <summary>
        ///     У тайла есть внутренний угол образованный двумя стенами, либо тайл имеет двух соседей, стены которых формируют внешний угол на нашем тайле
        ///     ---
        ///     The cyle has an inner corner formed by two walls, or the cyle has two neighbors, whose walls form the outer corner on our cyle
        /// </summary>
        public bool HasAnyCorner
        {
            get
            {
                return HasInnerCorner || HasOuterCorner;
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
                return LeftEdge != EdgeType.Empty ||
                       RightEdge != EdgeType.Empty ||
                       TopEdge != EdgeType.Empty ||
                       BottomEdge != EdgeType.Empty;
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

        public bool IsEmptyFurniture
        {
            get
            {
                return Maps.IsEmpty(furnitureData);
            }
        }
        
        #endregion
        
        #region Shared Methods

        /// <summary>
        ///     Устанавливает объект мебели на указанном сегменте
        ///     ---
        ///     Sets the furniture object on the specified segment
        /// </summary>
        /// <param name="layout">
        ///     Слой, на котором находится сегмент
        ///     ---
        ///     The layer on which the segment is located
        /// </param>
        /// <param name="segment">
        ///     Сегмент, на котором находится мебель
        ///     ---
        ///     The segment in which the furniture is located
        /// </param>
        /// <param name="item">
        ///     Объект мебели, который нужно расположить (если мебель нужно убрать, то null)
        ///     ---
        ///     Object of furniture to be placed (if furniture is to be removed, then null)
        /// </param>
        public void Set(TileLayoutType layout, TileSegmentType segment, IEnvironmentItem item)
        {
            var data = Get(layout);
            if (data == null)
            {
                data = new Dictionary<TileSegmentType, IEnvironmentItem>();
                furnitureData.Add(layout, data);
            }
            if (item == null)
            {
                data.Remove(segment);
                return;
            }
            data[segment] = item;
        }

        /// <summary>
        ///     Получает объект мебели, расположенный на указанном сегменте
        ///     ---
        ///     Gets the furniture object located on the specified segment
        /// </summary>
        /// <param name="layout">
        ///     Слой, на котором находится сегмент
        ///     ---
        ///     The layer on which the segment is located
        /// </param>
        /// <param name="segment">
        ///     Сегмент, на котором находится мебель
        ///     ---
        ///     The segment in which the furniture is located
        /// </param>
        /// <returns>
        ///     Объект мебели, если в указанном сегменте такой был расположен, или null, если в указанном сегменте на указанном слое не было расположено мебели
        ///     ---
        ///     A furniture object if the specified segment had one, or null if no furniture was located in the specified segment on the specified layer
        /// </returns>
        public IEnvironmentItem Get(TileLayoutType layout, TileSegmentType segment)
        {
            var data = Get(layout);
            if (data == null)
                return null;
            data.TryGetValue(segment, out var item);
            return item;
        }

        /// <summary>
        ///     Получает коллекцию сегментов на указанном слое
        ///     ---
        ///     Gets a collection of segments on the specified layer
        /// </summary>
        /// <param name="layout">
        ///     Слой на котором ищем сегменты
        ///     ---
        ///     The layer on which we are looking for segments
        /// </param>
        /// <returns>
        ///     Коллекцию занятых мебелью сегментов, или null - если на текущем слое нет ни одного занятого сегмента
        ///     ---
        ///     A collection of occupied furniture segments, or null - if there are no occupied segments on the current layer
        /// </returns>
        public IDictionary<TileSegmentType, IEnvironmentItem> Get(TileLayoutType layout)
        {
            furnitureData.TryGetValue(layout, out var data);
            return data;
        }
        
        #endregion
        
    }

}