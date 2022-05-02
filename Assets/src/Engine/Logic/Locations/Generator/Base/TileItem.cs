using System;
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

        public static readonly Func<TileSegmentLink, bool> EmptyEnvironmentItemFilter = (link => link.Item == null);
        
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

        public EdgeLayout GetLayout(TileLayoutType tileLayout)
        {
            switch (tileLayout)
            {
                case TileLayoutType.Ceiling:
                    return EdgeLayout.Ceiling;
                case TileLayoutType.Floor:
                    return EdgeLayout.Floor;
                case TileLayoutType.WallBottom:
                    return EdgeLayout.BottomInside;
                case TileLayoutType.WallLeft:
                    return EdgeLayout.LeftInside;
                case TileLayoutType.WallRight:
                    return EdgeLayout.RightInside;
                case TileLayoutType.WallTop:
                    return EdgeLayout.TopInside;
               default:
                    throw new NotSupportedException();
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
            furnitureData.TryGetValue(layout, out var data);
            if (data == null)
            {
                data = new Dictionary<TileSegmentType, IEnvironmentItem>();
                furnitureData[layout] = data;
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
            furnitureData.TryGetValue(layout, out var data);
            if (data == null)
                return null;
            data.TryGetValue(segment, out var item);
            return item;
        }
        
        /// <summary>
        ///     Выполняет поиск от указанного сегмента в указанном направлении
        ///     ---
        ///     Performs a search from a specified segment in a specified direction
        /// </summary>
        /// <param name="tileLayout">
        ///     Слой, на котором будет выполняться поиск
        ///     ---
        ///     The layer on which the search will be performed
        /// </param>
        /// <param name="start">
        ///     Начальный сегмент, от которого будет выполняться поиск
        ///     ---
        ///     Initial segment from which the search will be performed
        /// </param>
        /// <param name="direction">
        ///     Направление поиска
        ///     ---
        ///     Search Direction
        /// </param>
        /// <param name="deep">
        ///     Глубина поиска - количество сегментов в сторону поиска
        ///     ---
        ///     Depth of search - the number of segments in the search direction
        /// </param>
        /// <param name="filter">
        ///     Фильтр по которому будут отбираться ссылки на сегменты (по умолчанию выключен)
        ///     ---
        ///     Filter by which links to segments will be selected (off by default)
        /// </param>
        /// <returns>
        ///     Коллекция сегментов на слое layout, расположенных в сторону (глубина поиска deep сегментов) от start по направлению поиска direction
        ///     ---
        ///     A collection of segments on the layout layer, positioned sideways (depth of search of deep segments) from the start in the direction of the search
        /// </returns>
        public IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindDirection(TileLayoutType tileLayout, TileSegmentType start, TileFindDirection direction, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            TileSegmentType current = start;
            TileItem currentTile = this;
            
            for (int level = 0; level < deep; level++)
            {
                var segment = current.MoveByDirection(tileLayout, currentTile, direction, out currentTile);
                if (currentTile == null)
                    break;
                var layout = GetLayout(tileLayout);
                TryAddWithCheck(list,new TileSegmentLink()
                {
                    Tile = currentTile,
                    Layout = tileLayout,
                    Item = currentTile.Get(tileLayout, segment),
                    Marker = Marker,
                    SegmentType = segment,
                    EdgeType = FindEdge(layout, segment, direction),
                    EdgeLayout = layout,
                }, filter);
            }
            return list;
        }

        private EdgeType FindEdge(EdgeLayout layout, TileSegmentType segment, TileFindDirection direction)
        {
            if (layout == EdgeLayout.Ceiling || layout == EdgeLayout.Floor) // Сложный вариант, мы на полу, или потолке
            { // Чтобы понять рядом с какой гранью сегмент, нужно учесть направление
                switch (segment)
                {
                    case TileSegmentType.S00:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return TopEdge;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return LeftEdge;
                            default:
                                throw new NotSupportedException();
                        }
                    case TileSegmentType.S01:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return BottomEdge;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return LeftEdge;
                            default:
                                throw new NotSupportedException();
                        }
                    case TileSegmentType.S10:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return TopEdge;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return RightEdge;
                            default:
                                throw new NotSupportedException();
                        }
                    case TileSegmentType.S11:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return BottomEdge;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return RightEdge;
                            default:
                                throw new NotSupportedException();
                        }
                   default:
                       throw new NotSupportedException();
                }
            }
            return GetEdge(layout); // Всё просто, определяем грань по слою
        }

        /// <summary>
        ///     Выполняет двусторонний поиск от указанного сегмента в указанном направлении + обратном от него
        ///     ---
        ///     Performs a two-way search from a specified segment in the specified direction + backward from it
        /// </summary>
        /// <param name="tileLayout">
        ///     Слой, на котором будет выполняться поиск
        ///     ---
        ///     The layer on which the search will be performed
        /// </param>
        /// <param name="start">
        ///     Начальный сегмент, от которого будет выполняться поиск
        ///     ---
        ///     Initial segment from which the search will be performed
        /// </param>
        /// <param name="direction">
        ///     Начальное направление поиска
        ///     ---
        ///     Initial search direction
        /// </param>
        /// <param name="deep">
        ///     Глубина поиска - количество сегментов в одну сторону поиска
        ///     ---
        ///     Search depth - the number of segments in one side of the search
        /// </param>
        /// <param name="filter">
        ///     Фильтр по которому будут отбираться ссылки на сегменты (по умолчанию выключен)
        ///     ---
        ///     Filter by which links to segments will be selected (off by default)
        /// </param>
        /// <returns>
        ///     Коллекция сегментов на слое layout, расположенных (глубина поиска deep сегментов) от start по направлению поиска direction и в обратную сторону direction
        ///     ---
        ///     A collection of segments on a layout layer, located (depth of search of deep segments) from start in the direction of search direction and in the opposite direction direction
        /// </returns>
        public IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindBothDirection(TileLayoutType tileLayout, TileSegmentType start, TileFindDirection direction, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(tileLayout, start, direction, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(tileLayout, start, direction.Invert(), deep, filter));
            return list;
        }
        
        public IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindCrossDirection(TileLayoutType tileLayout, TileSegmentType start, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(tileLayout, start, TileFindDirection.Left, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(tileLayout, start, TileFindDirection.Right, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(tileLayout, start, TileFindDirection.Top, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(tileLayout, start, TileFindDirection.Bottom, deep, filter));
            return list;
        }

        /// <summary>
        ///     Получает все сегменты на слое layout рядом с edge (и пустые и заполненные)
        ///     ---
        ///     Gets all the segments on the layout layer next to the edge (both empty and filled)
        /// </summary>
        /// <returns>
        ///     Коллекция сегментов на слое layout, раясположенных рядом с edge
        ///     ---
        ///     A collection of segments on the layout layer, placed next to the edge
        /// </returns>
        public IList<TileSegmentLink> GetFurnitureOnTheLayoutByEdge(TileLayoutType layout, EdgeType edge, Func<TileSegmentLink, bool> filter = null)
        {
            var result = new List<TileSegmentLink>();
            if (HasNotAnyEdge && edge != EdgeType.Empty)
                return result;

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
                
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s00Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S00,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.LeftInside,
                }, filter);
                data.TryGetValue(TileSegmentType.S01, out var s01Value);
                s01 = true;
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s01Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S01,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.LeftInside,
                }, filter);
            }
            
            if (RightEdge == edge)
            {
                data.TryGetValue(TileSegmentType.S10, out var s10Value);
                s10 = true;
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s10Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S10,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.RightInside,
                }, filter);
                data.TryGetValue(TileSegmentType.S11, out var s11Value);
                s11 = true;
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = this,
                    Layout = layout,
                    Item = s11Value,
                    Marker = Marker,
                    SegmentType = TileSegmentType.S11,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.RightInside,
                }, filter);
            }
            
            if (TopEdge == edge)
            {
                if (!s11)
                {
                    data.TryGetValue(TileSegmentType.S11, out var s11Value);
                    TryAddWithCheck(result, new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s11Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S11,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.TopInside,
                    }, filter);
                }
                if (!s01)
                {
                    data.TryGetValue(TileSegmentType.S01, out var s01Value);
                    TryAddWithCheck(result, new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s01Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S01,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.TopInside,
                    }, filter);
                }
            }
            
            if (BottomEdge == edge)
            {
                if (!s00)
                {
                    data.TryGetValue(TileSegmentType.S00, out var s00Value);
                    TryAddWithCheck(result, new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s00Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S00,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.BottomInside,
                    }, filter);
                }
                if (!s10)
                {
                    data.TryGetValue(TileSegmentType.S10, out var s10Value);
                    TryAddWithCheck(result, new TileSegmentLink()
                    {
                        Tile = this,
                        Layout = layout,
                        Item = s10Value,
                        Marker = Marker,
                        SegmentType = TileSegmentType.S10,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.BottomInside,
                    }, filter);
                }
            }
            return result;
        }

        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">
        ///
        /// </param>
        /// <param name="item">
        ///
        /// </param>
        /// <param name="filter">
        ///
        /// </param>
        private void TryAddWithCheck(IList<TileSegmentLink> list, TileSegmentLink item, Func<TileSegmentLink, bool> filter)
        {
            if (filter != null && !filter(item))
                return; // Есть фильтрация, и она отсеила наш элемент, не добавляем его
            list.Add(item); // Либо фильтрации не было, либо мы подошли по её критериям
        }
        
    }

}