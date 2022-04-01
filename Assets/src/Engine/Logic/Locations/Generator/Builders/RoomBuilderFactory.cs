using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Engine.Data.Generation.Factories;
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

        /// <summary>
        ///     Builds a room by markers and forms a tile grid
        ///     ---
        ///     Performs construction of a room by markers
        /// </summary>
        /// <param name="context">
        ///     Контекст генерации помещения
        ///     ---
        ///     Room Generation Context
        /// </param>
        public void BuildRoom(GenerationRoomContext context)
        {
            var elementFactory = LocationTypeSuperFactory.Instance.Get(context.BuildInfo.LocationType);
            var buildType = context.BuildInfo.RoomType;
            var variationCount = elementFactory.GetCount(buildType);
            var buildingVariationID = new System.Random((int)context.BuildInfo.BuildID).Next(1, variationCount);
            context.BuildingElement = elementFactory.Get(buildType, buildingVariationID);

            foreach (var builder in builders)
                builder.Value.Build(context);

            // Формируем информацию о тайлах
            BuildTileGridInfo(context);
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
            if (list == null || list.Count == 0)
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
            if (list == null || list.Count == 0)
                return new List<IEnvironmentItem>();
            
            var currentFurnitureProcessorIndex = new System.Random((int)(context.BuildInfo.BuildID + context.BuildInfo.CurrentFloor)).Next(0, list.Count);
            var processor = list[currentFurnitureProcessorIndex];

            return processor.Create(context);
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
        private void BuildTileGridInfo(GenerationRoomContext context)
        {
            context.TilesInfo = new TileCellInfo()
            {
                TilesData = new List<TileItem>()
            };

            context.MarkersByType.TryGetValue(typeof(FloorMarker), out var floorMarkers);
            if (floorMarkers == null || floorMarkers.Count == 0)
                return; // Нет пола - нет сетки тайлов

            // Положение маркера к тайлу
            IDictionary<Vector3, TileItem> markerPosToMarker = floorMarkers.ToDictionary(marker => marker.Position, marker => new TileItem()
            {
                FurnitureData = new Dictionary<TileLayoutType, IDictionary<TileSegmentType, IEnvironmentItem>>(),
                Marker = (FloorMarker) marker,
            });
            
            // Пробегаемся по тайлам, и связываем соседей, чтобы понимать где стены
            foreach (var tile in markerPosToMarker.Values)
            {
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(-1, 0), out var leftTile);
                tile.LeftOfThis = leftTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(1, 0), out var rightTile);
                tile.RightOfThis = rightTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(0, -1), out var topTile);
                tile.TopOfThis = topTile;
                markerPosToMarker.TryGetValue(tile.Marker.Position + GetPosWithOffset(0, 1), out var bottomTile);
                tile.BottomOfThis = bottomTile;
            }

            // Остальные маркеры стен, окон и дверей
            foreach (var marker in context.AllMarkers)
            {
                if (marker is FloorMarker)
                    continue;
                UpdateEdgeLink(markerPosToMarker, marker, Direction.Left);
                UpdateEdgeLink(markerPosToMarker, marker, Direction.Right);
                UpdateEdgeLink(markerPosToMarker, marker, Direction.Top);
                UpdateEdgeLink(markerPosToMarker, marker, Direction.Bottom);
            }

            // Сохраняем собранную информацию
            context.TilesInfo.TilesData = markerPosToMarker.Values.ToList();
        }

        /// <summary>
        ///     Заполняет ссылки на граничащие объекты - стены, двери, окна.
        ///     На вход подаётся маркер, по маркеру находим тайлы, которые на нём расположены
        ///     ---
        ///     Fills in the references to the bordering objects - walls, doors, windows.
        ///     A marker is fed to the input, we use the marker to find the tiles that are located on it
        /// </summary>
        /// <param name="markerPosToMarker">
        ///     Мапа Позиция -> Тайл
        ///     ---
        ///     Map Position -> Tile
        /// </param>
        /// <param name="marker">
        ///     Проверяемый маркер
        ///     ---
        ///     Marker to be checked
        /// </param>
        /// <param name="direction">
        ///     Направление, по которому ищем соседа-тайла
        ///     ---
        ///     The direction in which to look for a neighbor-tile
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     Недействительное направление
        ///     ---
        ///     Invalid direction value
        /// </exception>
        private static void UpdateEdgeLink(IDictionary<Vector3, TileItem> markerPosToMarker, IMarker marker, Direction direction)
        {
            var offset = GetOffset(direction);
            if (!markerPosToMarker.TryGetValue(marker.Position + GetPosWithOffset(offset.x, offset.y), out var tile))
                return;

            var edge = new EdgeItem()
            {
                Marker = marker,
                EdgeType = GetEdgeType(marker.GetType()),
            };

            if (edge.EdgeType == EdgeType.Empty && tile.IsSurrounded)
                edge.EdgeType = EdgeType.Floor;

            switch (direction)
            {
                case Direction.Left:
                    tile.LeftEdge = edge;
                    break;
                case Direction.Right:
                    tile.RightEdge = edge;
                    break;
                case Direction.Top:
                    tile.TopEdge = edge;
                    break;
                case Direction.Bottom:
                    tile.BottomEdge = edge;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///     Получает тип граничащего объекта по типу маркера
        ///     ---
        ///     Gets the type of the bordering object by marker type
        /// </summary>
        /// <param name="type">
        ///     Тип маркера
        ///     ---
        ///     Type of marker
        /// </param>
        /// <returns>
        ///     Тип грани
        ///     ---
        ///     Edge type
        /// </returns>
        private static EdgeType GetEdgeType(Type type)
        {
            if (type == typeof(WallMarker))
                return EdgeType.Wall;
            if (type == typeof(WallWithDoorMarker))
                return EdgeType.Door;
            if (type == typeof(WallWithWindowMarker))
                return EdgeType.Window;
            return EdgeType.Empty;
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

        /// <summary>
        ///     Выполняет преобразование направление в смещение
        ///     ---
        ///     Converts direction to offset
        /// </summary>
        /// <param name="direction">
        ///     Направление в котором нужно искать соседний тайл
        ///     ---
        ///     The direction in which to look for a neighboring tile
        /// </param>
        /// <returns>
        ///     Смещение по осям
        ///     ---
        ///     Axis Offset by X,Z (x,y)
        /// </returns>
        /// <exception cref="NotSupportedException">
        ///     Недействительное направление
        ///     ---
        ///     Invalid direction value
        /// </exception>
        private static Vector2Int GetOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:   return new Vector2Int(-1, 0);
                case Direction.Right:  return new Vector2Int(1, 0);
                case Direction.Top:    return new Vector2Int(0, -1);
                case Direction.Bottom: return new Vector2Int(0, 1);
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///
        ///     Варианты направлений в сторону соседних тайлов относительно текущего
        ///     ---
        ///     Options for directions to neighboring tiles relative to the current one
        /// 
        /// </summary>
        private enum Direction : byte
        {
            Left,
            Right,
            Top,
            Bottom,
        };

    }

}
