using Engine.Data;
using Engine.Generator;
using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using UnityEngine;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Генератор зданий
    /// Выполняет необходимую генерацию указанного этажа
    /// ---
    /// Building generator
    /// Performs the necessary generation of the specified floor
    /// 
    /// </summary>
    public static class LocationGenerateContex
    {

        #region Constants

        /// <summary>
        ///     Размер сети, для перемещения маркеров
        ///     ---
        ///     Grid size, to move the markers
        /// </summary>
        public static Vector3 EDITOR_WEB_SIZE = new Vector3(0.5f, 0.5f, 0.5f);

        /// <summary>
        ///     Размер тайла для напольного покрытия
        ///     ---
        ///     Tile size for flooring
        /// </summary>
        public static Vector3 FLOOR_TILE_SIZE = new Vector3(2.5f, 0f, 2.5f);

        /// <summary>
        ///     Размер тайла для стен
        ///     ---
        ///     Tile size for walls
        /// </summary>
        public static Vector3 WALL_TILE_SIZE = new Vector3(2.5f, 3f, 0.15f);

        #endregion

        #region Methods

        /// <summary>
        ///     Генерация информации о здании и помещениях
        ///     ---
        ///     Generating building and room information
        /// </summary>
        /// <param name="locationInfo">
        ///     Локация для которой совершается генерация
        ///     ---
        ///     Location for which the generation is performed
        /// </param>
        /// <returns>
        ///     Сгенерированные данные для указанного этажа здания
        ///     В эту информацию входят сведения о самом здании, о этаже, помещениях, их наполнении
        ///     ---
        ///     Generated data for the specified floor of the building
        ///     This information includes information about the building itself, the floor, the rooms, their occupancy
        /// </returns>
        public static BuildLocationInfo Generate(Engine.Generator.LocationInfo locationInfo)
        {
            var result = new BuildLocationInfo();
            result.BuildID = locationInfo.ID;
            var percent = LocationInfoGenerator.MAX_BUILD_HEIGHT - LocationInfoGenerator.MIN_BUILD_HEIGHT * 0.01f;

            result.FloorsCount = 1 + Mathf.RoundToInt(locationInfo.Height * percent * 7);
            result.CurrentFloor = 1; // FIXME: текущий этаж

            result.LocationType = LocationType.Living;
            result.RoomType = RoomType.Blank;

            return result;
        }

        /// <summary>
        ///     Выполняет генерацию помещения по маркерам.
        ///     В результате генерации мы получим полноценное помещение в сцене.
        ///     ---
        ///     Performs room generation by markers.
        ///     As a result of the generation we will get a full room in the scene.
        /// </summary>
        /// <param name="markers">
        ///     Маркеры, формирующие помещение, которое надо будет сгенерировать
        ///     ---
        ///     Markers that form the room to be generated
        /// </param>
        public static void GenerateByMarkers(ICollection<IMarker> markers)
        {

            var markersByGroup = new Dictionary<Type, IList<IMarker>>();
            foreach(IMarker marker in markers)
                markersByGroup.AddInToList(marker.GetType(), marker);

            var context = new GenerationRoomContext()
            {
                AllMarkers = markers,
                BuildInfo = Game.Instance.Runtime.GenerationInfo,
                FurnitureInfo = new BuildFurnitureInfo(),
                MarkersByType = markersByGroup,
                RoomKindType = RoomKindType.Kitchen,
            };
            
            context.BuildRandom = new System.Random((int)context.BuildInfo.BuildID);
            context.FloorRandom = new System.Random((int)context.BuildInfo.BuildID + (int)context.BuildInfo.CurrentFloor);
            context.RoomRandom = new System.Random((int)context.BuildInfo.BuildID + (int)context.BuildInfo.CurrentFloor + (int)context.RoomKindType);

            // Генерация помещения
            RoomBuilderFactory.Instance.BuildRoom(context);

            // Заполнение помещения объектами
            RoomBuilderFactory.Instance.BuildRoomObjects(context);
        }

        #endregion

    }

}
