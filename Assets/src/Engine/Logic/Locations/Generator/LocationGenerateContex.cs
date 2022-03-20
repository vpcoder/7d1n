using Engine.Data;
using Engine.Generator;
using Engine.Logic.Locations.Generator.Markers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Генератор зданий
    /// Выполняет необходимую генерацию указанного этажа
    /// ---
    /// 
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
        /// 
        /// </summary>
        /// <param name="locationInfo">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public static BuildLocationInfo Generate(Engine.Generator.LocationInfo locationInfo)
        {
            var result = new BuildLocationInfo();
            result.BuildID = locationInfo.ID;
            var percent = LocationInfoGenerator.MAX_BUILD_HEIGHT - LocationInfoGenerator.MIN_BUILD_HEIGHT * 0.01f;
            result.FloorsCount = 1 + Mathf.RoundToInt(locationInfo.Height * percent * 7);
            result.LocationType = LocationType.Living;
            result.RoomType = RoomType.Living;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="markers">
        /// 
        /// </param>
        public static void GenerateByMarkers(IEnumerable<IMarker> markers)
        {
            var markersByGroup = new Dictionary<Type, IList<IMarker>>();
            foreach(IMarker marker in markers)
                markersByGroup.AddInToList(marker.GetType(), marker);

            var context = new GenerationBuildContext()
            {
                AllMarkers = markers,
                BuildInfo = Game.Instance.Runtime.GenerationInfo,
                MarkersByType = markersByGroup,
            };

            BuilderBuildFactory.Instance.Build(context);
        }

        #endregion

    }

}
