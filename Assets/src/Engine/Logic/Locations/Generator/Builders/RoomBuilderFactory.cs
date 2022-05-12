using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Engine.Logic.Locations.Generator.Builders;
using Engine.Logic.Locations.Generator.Environment.Building.Arrangement;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using src.Engine.Logic.Locations.Generator.Furniture;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Фабрика выполняющая построение помещений по маркерам
    /// ---
    /// A factory that performs construction of rooms by markers
    /// 
    /// </summary>
    public class RoomBuilderFactory
    {

        #region Singleton

        private static Lazy<RoomBuilderFactory> instance = new Lazy<RoomBuilderFactory>(() => new RoomBuilderFactory());

        public static RoomBuilderFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private RoomBuilderFactory()
        {
            foreach(var builder in AssembliesHandler.CreateImplementations<IBuilder>())
                builders.Add(builder.MarkerType, builder);
        }

        #endregion

        /// <summary>
        ///     Коллекция билдеров, которые умеют работать с маркерами
        ///     ---
        ///     A collection of builders who know how to work with markers
        /// </summary>
        private IDictionary<Type, IBuilder> builders = new Dictionary<Type, IBuilder>();

        public void BuildGlobalScene(BuildLocationGlobalInfo context)
        {
            BuildGlobalTileGridInfo(context);
            
            // Формируем помещение
            foreach (var builder in builders)
                builder.Value.Build(context);
        }

        /// <summary>
        ///     Выполняет генерацию и заполнение помещения объектами
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        public void BuildRoomObjects(GenerationRoomContext context)
        {
            // Формируем мебель и объекты интерьера в помещении
            context.FurnitureInfo.FurnitureItems = BuildFurnitureInfo(context);
            
            // Выполняем заполнение помещения
            DoArrangementFurniture(context);
        }

        /// <summary>
        ///     Выполняет генерацию мебели и заполнение ею помещения
        ///     ---
        ///     A collection of generated furniture for this room
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        private void DoArrangementFurniture(GenerationRoomContext context)
        {
            var list = ArrangementSuperFactory.Instance.Get(context.RoomKindType);
            if (Lists.IsEmpty(list))
                return;
            
            var currentArrangementIndex = new System.Random((int)(context.BuildInfo.BuildID + context.BuildInfo.CurrentFloor)).Next(0, list.Count);
            var processor = list[currentArrangementIndex];
            
            processor.ArrangementProcess(context, context.FurnitureInfo.FurnitureItems);
        }
        
        /// <summary>
        ///     Выполняет генерацию мебели для помещения
        ///     ---
        ///     Furniture factory for a given room
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        /// <returns>
        ///     Коллекция сгенерированной мебели для этого помещения
        ///     ---
        ///     A collection of generated furniture for this room
        /// </returns>
        private ICollection<IEnvironmentItem> BuildFurnitureInfo(GenerationRoomContext context)
        {
            var list = FurnitureProcessorSuperFactory.Instance.Get(context.RoomKindType);
            if (Lists.IsEmpty(list))
                return new List<IEnvironmentItem>();
            
            var currentFurnitureProcessorIndex = context.FloorRandom.Next(0, list.Count);
            var processor = list[currentFurnitureProcessorIndex];

            return processor.Create(context);
        }
        
        public void BuildGlobalTileGridInfo(BuildLocationGlobalInfo context)
        {
            var floorMarkers = context.Markers
                .Select(marker => marker as FloorMarker)
                .Where(marker => marker != null)
                .ToList();
            
            if (Lists.IsEmpty(floorMarkers))
                return; // Нет пола - нет сетки тайлов

            // Положение маркера к тайлу
            IDictionary<Vector3, TileItem> markerPosToMarker = floorMarkers.ToDictionary(marker => marker.Position, marker => new TileItem()
            {
                Marker = marker,
                LeftEdge = marker.LeftEdge,
                RightEdge = marker.RightEdge,
                BottomEdge = marker.BottomEdge,
                TopEdge = marker.TopEdge,
            });
            
            // Пробегаемся по тайлам, и связываем соседей, чтобы понимать где стены
            foreach (var tile in markerPosToMarker.Values)
            {
                tile.Marker.Tile = tile;
                
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(-1, 0), out var topTile);
                tile.TopOfThis = topTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(1, 0), out var bottomTile);
                tile.BottomOfThis = bottomTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(0, -1), out var leftTile);
                tile.LeftOfThis = leftTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(0, 1), out var rightTile);
                tile.RightOfThis = rightTile;
                
                if ((leftTile == null || leftTile.Marker.RoomHierarchy != tile.Marker.RoomHierarchy) && tile.LeftEdge == EdgeType.Empty)
                    tile.LeftEdge = leftTile != null && leftTile.RightEdge != EdgeType.Empty ? leftTile.RightEdge : EdgeType.Wall;

                if ((rightTile == null || rightTile.Marker.RoomHierarchy != tile.Marker.RoomHierarchy) && tile.RightEdge == EdgeType.Empty)
                    tile.RightEdge = rightTile != null && rightTile.LeftEdge != EdgeType.Empty ? rightTile.LeftEdge : EdgeType.Wall;
                
                if ((topTile == null || topTile.Marker.RoomHierarchy != tile.Marker.RoomHierarchy) && tile.TopEdge == EdgeType.Empty)
                    tile.TopEdge = topTile != null && topTile.BottomEdge != EdgeType.Empty ? topTile.BottomEdge : EdgeType.Wall;
                
                if ((bottomTile == null || bottomTile.Marker.RoomHierarchy != tile.Marker.RoomHierarchy) && tile.BottomEdge == EdgeType.Empty)
                    tile.BottomEdge = bottomTile != null && bottomTile.TopEdge != EdgeType.Empty ? bottomTile.TopEdge : EdgeType.Wall;
            }
        }
        
        /// <summary>
        ///     Выполняет формирование информации о тайлах помещения.
        ///     Сначала пробегаемся по всем маркерам пола, затем определяем что на границах тайлов, выполняем связывание соседних маркеров
        ///     ---
        ///     Performs formation of information about the room tiles.
        ///     First we run through all the floor markers, then we determine what is on the borders of the tiles, then we bind neighboring markers
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        public void BuildTileGridInfo(GenerationRoomContext context)
        {
            context.TilesInfo = new TileCellInfo()
            {
                TilesData = new List<TileItem>()
            };

            context.MarkersByTypeOnRoom.TryGetValue(typeof(FloorMarker), out var floorMarkers);
            if (Lists.IsEmpty(floorMarkers))
                return; // Нет пола - нет сетки тайлов

            // Положение маркера к тайлу
            IDictionary<Vector3, TileItem> markerPosToMarker = floorMarkers.ToDictionary(marker => marker.Position, marker => new TileItem()
            {
                Marker = (FloorMarker) marker,
            });
            
            // Пробегаемся по тайлам, и связываем соседей, чтобы понимать где стены
            foreach (var tile in markerPosToMarker.Values)
            {
                tile.Marker.Tile = tile;
                tile.LeftEdge   = tile.Marker.LeftEdge;
                tile.RightEdge  = tile.Marker.RightEdge;
                tile.BottomEdge = tile.Marker.BottomEdge;
                tile.TopEdge    = tile.Marker.TopEdge;
                
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(-1, 0), out var topTile);
                tile.TopOfThis = topTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(1, 0), out var bottomTile);
                tile.BottomOfThis = bottomTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(0, -1), out var leftTile);
                tile.LeftOfThis = leftTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(0, 1), out var rightTile);
                tile.RightOfThis = rightTile;

                if (tile.LeftOfThis == null && tile.LeftEdge == EdgeType.Empty)
                    tile.LeftEdge = EdgeType.Wall;
                
                if (tile.RightOfThis == null && tile.RightEdge == EdgeType.Empty)
                    tile.RightEdge = EdgeType.Wall;
                
                if (tile.TopOfThis == null && tile.TopEdge == EdgeType.Empty)
                    tile.TopEdge = EdgeType.Wall;
                
                if (tile.BottomOfThis == null && tile.BottomEdge == EdgeType.Empty)
                    tile.BottomEdge = EdgeType.Wall;
            }

            foreach (var tile in markerPosToMarker.Values)
            {
                if (tile.LeftOfThis != null && tile.LeftEdge != EdgeType.Empty && tile.LeftEdge != EdgeType.Window)
                {
                    tile.LeftOfThis.RightEdge        = tile.LeftEdge;
                    tile.LeftOfThis.Marker.RightEdge = tile.LeftEdge;
                }

                if (tile.RightOfThis != null && tile.RightEdge != EdgeType.Empty && tile.RightEdge != EdgeType.Window)
                {
                    tile.RightOfThis.LeftEdge        = tile.RightEdge;
                    tile.RightOfThis.Marker.LeftEdge = tile.RightEdge;
                }

                if (tile.TopOfThis != null && tile.TopEdge != EdgeType.Empty && tile.TopEdge != EdgeType.Window)
                {
                    tile.TopOfThis.BottomEdge        = tile.TopEdge;
                    tile.TopOfThis.Marker.BottomEdge = tile.TopEdge;
                }

                if (tile.BottomOfThis != null && tile.BottomEdge != EdgeType.Empty && tile.BottomEdge != EdgeType.Window)
                {
                    tile.BottomOfThis.TopEdge        = tile.BottomEdge;
                    tile.BottomOfThis.Marker.TopEdge = tile.BottomEdge;
                }
            }
            
            // Сохраняем собранную информацию
            context.TilesInfo.TilesData = markerPosToMarker.Values.ToList();
        }

        /// <summary>
        ///     Преобразует смещение по осям в смещение позиции в пространстве в единицах unity
        ///     ---
        ///     Converts the axis displacement into a position offset in space in units of unity
        /// </summary>
        /// <param name="offsetX">
        ///     Смещение по оси X значения -1,0,+1
        ///     ---
        ///     X-axis offset values -1,0,+1
        /// </param>
        /// <param name="offsetZ">
        ///     Смещение по оси Z значения -1,0,+1
        ///     ---
        ///     Z-axis offset values -1,0,+1
        /// </param>
        /// <returns>
        ///     Unity позиция в виде Vector3
        ///     ---
        ///     Unity Vector3 Position
        /// </returns>
        private static Vector3 GetPosWithOffset(int offsetX, int offsetZ)
        {
            return new Vector3(offsetX * LocationGenerateContex.FLOOR_TILE_SIZE.x, 0f, offsetZ * LocationGenerateContex.FLOOR_TILE_SIZE.z);
        }

    }

}
