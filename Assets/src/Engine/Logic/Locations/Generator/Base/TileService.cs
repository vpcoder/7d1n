using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;

namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    ///
    /// Сервис для работы с тайловой сеткой, помогает выполнять сложные запросы по сетке
    /// ---
    /// Service for working with the tile grid, helps to perform complex queries on the grid
    /// 
    /// </summary>
    public class TileService
    {
        
        public static readonly Func<TileSegmentLink, bool> EmptyEnvironmentItemFilter = (link => link.Item == null);

        /// <summary>
        ///     Преобразует грань в направление вдоль этой грани
        ///     ---
        ///     Converts an edge into a direction along that edge
        /// </summary>
        /// <param name="layout">
        ///     Грань, по которой необходимо получить направление
        ///     ---
        ///     The facet where you need to get a referral
        /// </param>
        /// <returns>
        ///     Направление вдоль указанной грани
        ///     ---
        ///     The direction along the indicated edge
        /// </returns>
        public static TileFindDirection AlongsideDirection(EdgeLayout layout)
        {
            switch (layout)
            {
                case EdgeLayout.BottomInside:
                case EdgeLayout.BottomOutside:
                case EdgeLayout.TopInside:
                case EdgeLayout.TopOutside:
                    return TileFindDirection.Left;
                default:
                    return TileFindDirection.Top;
            }
        }
        
        public static EdgeLayout GetLayout(TileLayoutType tileLayout)
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
        ///     Выполняет поиск от указанного сегмента в указанном направлении
        ///     ---
        ///     Performs a search from a specified segment in a specified direction
        /// </summary>
        /// <param name="currentTile">
        ///     Начальный тайл, на котором выполняется поиск
        ///     ---
        ///     Initial tile, on which the search is performed
        /// </param>
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
        public static IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindDirection(TileItem currentTile, TileLayoutType tileLayout, TileSegmentType start, TileFindDirection direction, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            TileSegmentType current = start;
            
            for (int level = 0; level < deep; level++)
            {
                var segment = current.MoveByDirection(tileLayout, currentTile, direction, out currentTile);
                if (currentTile == null)
                    break;
                var layout = GetLayout(tileLayout);
                var edge = FindEdge(layout, segment, direction);
                TryAddWithCheck(list,new TileSegmentLink()
                {
                    Tile = currentTile,
                    Layout = tileLayout,
                    Item = currentTile.Get(tileLayout, segment),
                    Marker = currentTile.Marker,
                    SegmentType = segment,
                    EdgeType = currentTile.GetEdge(edge),
                    EdgeLayout = edge,
                }, filter);
                current = segment;
            }
            return list;
        }

        private static EdgeLayout FindEdge(EdgeLayout layout, TileSegmentType segment, TileFindDirection direction)
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
                                return EdgeLayout.BottomInside;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return EdgeLayout.LeftInside;
                            default:
                                throw new NotSupportedException();
                        }
                    case TileSegmentType.S01:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return EdgeLayout.TopInside;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return EdgeLayout.LeftInside;
                            default:
                                throw new NotSupportedException();
                        }
                    case TileSegmentType.S10:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return EdgeLayout.BottomInside;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return EdgeLayout.RightInside;
                            default:
                                throw new NotSupportedException();
                        }
                    case TileSegmentType.S11:
                        switch (direction)
                        {
                            case TileFindDirection.Left:
                            case TileFindDirection.Right:
                                return EdgeLayout.TopInside;
                            case TileFindDirection.Top:
                            case TileFindDirection.Bottom:
                                return EdgeLayout.RightInside;
                            default:
                                throw new NotSupportedException();
                        }
                   default:
                       throw new NotSupportedException();
                }
            }
            return layout; // Всё просто, определяем грань по слою
        }

        /// <summary>
        ///     Выполняет двусторонний поиск от указанного сегмента в указанном направлении + обратном от него
        ///     ---
        ///     Performs a two-way search from a specified segment in the specified direction + backward from it
        /// </summary>
        /// <param name="currentTile">
        ///     Начальный тайл, на котором выполняется поиск
        ///     ---
        ///     Initial tile, on which the search is performed
        /// </param>
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
        public static IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindBothDirection(TileItem currentTile, TileLayoutType tileLayout, TileSegmentType start, TileFindDirection direction, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(currentTile, tileLayout, start, direction, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(currentTile, tileLayout, start, direction.Invert(), deep, filter));
            return list;
        }
        
        public static IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindCrossDirection(TileItem currentTile, TileLayoutType tileLayout, TileSegmentType start, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(currentTile, tileLayout, start, TileFindDirection.Left, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(currentTile, tileLayout, start, TileFindDirection.Right, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(currentTile, tileLayout, start, TileFindDirection.Top, deep, filter));
            list.AddRange(GetFurnitureOnTheLayoutByFindDirection(currentTile, tileLayout, start, TileFindDirection.Bottom, deep, filter));
            return list;
        }

        public static IList<TileSegmentLink> GetFurnitureOnTheLayoutByFindCorner(TileSegmentLink cornerLink, int deep, Func<TileSegmentLink, bool> filter = null)
        {
            var direction = AlongsideDirection(cornerLink.EdgeLayout);
            // Вращаем направление на 90 градусов
            direction = (direction == TileFindDirection.Left || direction == TileFindDirection.Right) ? TileFindDirection.Top : TileFindDirection.Left;
            return GetFurnitureOnTheLayoutByFindBothDirection(cornerLink.Tile, cornerLink.Layout,
                cornerLink.SegmentType, direction, deep, filter);
        }

        public static IList<TileSegmentLink> GetFurnitureOnTheLayoutByTiles(TileLayoutType tileLayout, ICollection<TileItem> tiles, Func<TileSegmentLink, bool> filter = null)
        {
            var list = new List<TileSegmentLink>();
            foreach (var tile in tiles)
            {
                var link = new TileSegmentLink()
                {
                    Layout = tileLayout,
                    Tile = tile,
                    Marker = tile.Marker,
                    SegmentType = TileSegmentType.S00,
                    EdgeLayout = EdgeLayout.LeftInside,
                    EdgeType = tile.GetEdge(EdgeLayout.LeftInside)
                };
                TryAddWithCheck(list, link, filter);
                link = new TileSegmentLink()
                {
                    Layout = tileLayout,
                    Tile = tile,
                    Marker = tile.Marker,
                    SegmentType = TileSegmentType.S01,
                    EdgeLayout = EdgeLayout.TopInside,
                    EdgeType = tile.GetEdge(EdgeLayout.TopInside)
                };
                TryAddWithCheck(list, link, filter);
                link = new TileSegmentLink()
                {
                    Layout = tileLayout,
                    Tile = tile,
                    Marker = tile.Marker,
                    SegmentType = TileSegmentType.S10,
                    EdgeLayout = EdgeLayout.BottomInside,
                    EdgeType = tile.GetEdge(EdgeLayout.BottomInside)
                };
                TryAddWithCheck(list, link, filter);
                link = new TileSegmentLink()
                {
                    Layout = tileLayout,
                    Tile = tile,
                    Marker = tile.Marker,
                    SegmentType = TileSegmentType.S11,
                    EdgeLayout = EdgeLayout.RightInside,
                    EdgeType = tile.GetEdge(EdgeLayout.RightInside)
                };
                TryAddWithCheck(list, link, filter);
            }
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
        public static IList<TileSegmentLink> GetFurnitureOnTheLayoutByEdge(TileItem currentTile, TileLayoutType layout, EdgeType edge, Func<TileSegmentLink, bool> filter = null)
        {
            var result = new List<TileSegmentLink>();
            if (currentTile.HasNotAnyEdge && edge != EdgeType.Empty)
                return result;

            var data = currentTile.Get(layout) ?? new Dictionary<TileSegmentType, IEnvironmentItem>();
            
            var s00 = false;
            var s01 = false;
            var s10 = false;
            var s11 = false;
            
            if (currentTile.LeftEdge == edge)
            {
                data.TryGetValue(TileSegmentType.S00, out var s00Value);
                s00 = true;
                
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = currentTile,
                    Layout = layout,
                    Item = s00Value,
                    Marker = currentTile.Marker,
                    SegmentType = TileSegmentType.S00,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.LeftInside,
                }, filter);
                data.TryGetValue(TileSegmentType.S01, out var s01Value);
                s01 = true;
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = currentTile,
                    Layout = layout,
                    Item = s01Value,
                    Marker = currentTile.Marker,
                    SegmentType = TileSegmentType.S01,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.LeftInside,
                }, filter);
            }
            
            if (currentTile.RightEdge == edge)
            {
                data.TryGetValue(TileSegmentType.S10, out var s10Value);
                s10 = true;
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = currentTile,
                    Layout = layout,
                    Item = s10Value,
                    Marker = currentTile.Marker,
                    SegmentType = TileSegmentType.S10,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.RightInside,
                }, filter);
                data.TryGetValue(TileSegmentType.S11, out var s11Value);
                s11 = true;
                TryAddWithCheck(result, new TileSegmentLink()
                {
                    Tile = currentTile,
                    Layout = layout,
                    Item = s11Value,
                    Marker = currentTile.Marker,
                    SegmentType = TileSegmentType.S11,
                    EdgeType = edge,
                    EdgeLayout = EdgeLayout.RightInside,
                }, filter);
            }
            
            if (currentTile.TopEdge == edge)
            {
                if (!s11)
                {
                    data.TryGetValue(TileSegmentType.S11, out var s11Value);
                    TryAddWithCheck(result, new TileSegmentLink()
                    {
                        Tile = currentTile,
                        Layout = layout,
                        Item = s11Value,
                        Marker = currentTile.Marker,
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
                        Tile = currentTile,
                        Layout = layout,
                        Item = s01Value,
                        Marker = currentTile.Marker,
                        SegmentType = TileSegmentType.S01,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.TopInside,
                    }, filter);
                }
            }
            
            if (currentTile.BottomEdge == edge)
            {
                if (!s00)
                {
                    data.TryGetValue(TileSegmentType.S00, out var s00Value);
                    TryAddWithCheck(result, new TileSegmentLink()
                    {
                        Tile = currentTile,
                        Layout = layout,
                        Item = s00Value,
                        Marker = currentTile.Marker,
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
                        Tile = currentTile,
                        Layout = layout,
                        Item = s10Value,
                        Marker = currentTile.Marker,
                        SegmentType = TileSegmentType.S10,
                        EdgeType = edge,
                        EdgeLayout = EdgeLayout.BottomInside,
                    }, filter);
                }
            }
            return result;
        }

        /// <summary>
        ///     Выполняет проверку, подходит ли элемент item по фильтру filter, если да, то элемент добавляется в коллекцию list
        ///     ---
        ///     Checks if the item matches the filter, and if so, the item is added to the list collection
        /// </summary>
        /// <param name="list">
        ///     Коллекция в которую необходимо добавлять элемент
        ///     ---
        ///     A collection to which an item should be added
        /// </param>
        /// <param name="item">
        ///     Добавляемый элемент
        ///     ---
        ///     Item to be added
        /// </param>
        /// <param name="filter">
        ///     Фильтр выполняющий отсев элементов (может быть null - если так, то считается что фильтра нет, и в коллекцию добавляются все элементы)
        ///     ---
        ///     A filter that sifts items (can be null - if so, it is assumed that there is no filter, and all items are added to the collection)
        /// </param>
        private static void TryAddWithCheck(IList<TileSegmentLink> list, TileSegmentLink item, Func<TileSegmentLink, bool> filter)
        {
            if (filter != null && !filter(item))
                return; // Есть фильтрация, и она отсеила наш элемент, не добавляем его
            list.Add(item); // Либо фильтрации не было, либо мы подошли по её критериям
        }
        
    }
    
}