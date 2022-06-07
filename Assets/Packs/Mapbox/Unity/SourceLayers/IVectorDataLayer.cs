using System;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

namespace Mapbox.Unity.SourceLayers
{
	
	public interface IVectorDataLayer : ILayer
	{
		#region Layer Level APIs
		TileJsonData GetTileJsonData();


		/// <summary>
		/// Gets the `Data Source` for the `MAP LAYERS` section.
		/// </summary>
		void SetLayerSource(VectorSourceType vectorSource);

		/// <summary>
		/// Adds the provided `Data Source` (`Tileset ID`) to existing ones. For multiple
		/// sources, you can separate with a comma. `Tileset ID` string is added at the
		/// end of the existing sources.
		/// </summary>
		/// <param name="vectorSource">`Data Source` (`Tileset ID`) to add to existing sources.</param>
		void AddLayerSource(string vectorSource);

		/// <summary>
		/// Sets the layer source as Style-optimized vector tiles
		/// </summary>
		/// <param name="vectorSource">Vector source.</param>
		/// <param name="styleId">Style-Optimized style id.</param>
		/// <param name="modifiedDate">Modified date.</param>
		/// <param name="styleName">Style name.</param>
		void SetLayerSourceWithOptimizedStyle(string vectorSource, string styleId, string modifiedDate, string styleName = null);

		/// <summary>
		/// Sets the layer source as Style-optimized vector tiles
		/// </summary>
		/// <param name="vectorSource">Vector source.</param>
		/// <param name="styleId">Style-Optimized style id.</param>
		/// <param name="modifiedDate">Modified date.</param>
		/// <param name="styleName">Style name.</param>
		void SetLayerSourceWithOptimizedStyle(VectorSourceType vectorSource, string styleId, string modifiedDate, string styleName = null);

		/// <summary>
		/// Enables coroutines for vector features. Processes the specified amount
		/// of them each frame.
		/// </summary>
		/// <param name="entityPerCoroutine">Numbers of features to process each frame.</param>
		void EnableVectorFeatureProcessingWithCoroutines(int entityPerCoroutine = 20);

		/// <summary>
		/// Disables processing of vector features on coroutines.
		/// </summary>
		void DisableVectorFeatureProcessingWithCoroutines();
		#endregion

		#region LayerOperations

		// FEATURE LAYER OPERATIONS

		void AddFeatureSubLayer(VectorSubLayerProperties subLayerProperties);

		/// <summary>
		/// Adds a sub layer to render polygon features.
		/// Default settings include :
		/// Extrusion = true
		/// ExtrusionType = PropertyHeight
		/// ExtrusionGeometryType = Roof And Sides
		/// Testuring = Realistic.
		/// </summary>
		/// <param name="assignedSubLayerName">Assigned sub layer name.</param>
		/// <param name="dataLayerNameInService">Data layer name in service.</param>
		void AddPolygonFeatureSubLayer(string assignedSubLayerName, string dataLayerNameInService);

		/// <summary>
		/// Adds a sub layer to render line features.
		/// Default settings include :
		/// LineWidth = 1
		/// Extrusion = true
		/// ExtrusionType = AbsoluteHeight
		/// ExtrusionGeometryType = Roof And Sides
		/// Testuring = Dark.
		/// </summary>
		/// <param name="assignedSubLayerName">Assigned sub layer name.</param>
		/// <param name="dataLayerNameInService">Data layer name in service.</param>
		/// <param name="lineWidth">Line width.</param>
		void AddLineFeatureSubLayer(string assignedSubLayerName, string dataLayerNameInService, float lineWidth = 1);

		/// <summary>
		/// Adds a sub layer to render point features.
		/// </summary>
		/// <param name="assignedSubLayerName">Assigned sub layer name.</param>
		/// <param name="dataLayerNameInService">Data layer name in service.</param>
		void AddPointFeatureSubLayer(string assignedSubLayerName, string dataLayerNameInService);

		/// <summary>
		/// Adds feature sub layer for rendering using a custom pipeline.
		/// Custom Feature Sub Layer should be used with custom modifiers to leverage the layer data or render it using a non-standard pipeline.
		/// </summary>
		/// <param name="assignedSubLayerName">Assigned sub layer name.</param>
		/// <param name="dataLayerNameInService">Data layer name in service.</param>
		void AddCustomFeatureSubLayer(string assignedSubLayerName, string dataLayerNameInService);

		IEnumerable<VectorSubLayerProperties> GetAllFeatureSubLayers();

		IEnumerable<VectorSubLayerProperties> GetAllPolygonFeatureSubLayers();

		IEnumerable<VectorSubLayerProperties> GetAllLineFeatureSubLayers();

		IEnumerable<VectorSubLayerProperties> GetAllPointFeatureSubLayers();

		IEnumerable<VectorSubLayerProperties> GetFeatureSubLayerByQuery(Func<VectorSubLayerProperties, bool> query);

		VectorSubLayerProperties GetFeatureSubLayerAtIndex(int i);

		VectorSubLayerProperties FindFeatureSubLayerWithName(string featureLayerName);

		void RemoveFeatureSubLayerWithName(string featureLayerName);

		void RemoveFeatureSubLayer(VectorSubLayerProperties layer);

		// POI LAYER OPERATIONS

		void AddPointsOfInterestSubLayer(PrefabItemOptions poiLayerProperties);

		IEnumerable<PrefabItemOptions> GetAllPointsOfInterestSubLayers();

		PrefabItemOptions GetPointsOfInterestSubLayerAtIndex(int i);

		IEnumerable<PrefabItemOptions> GetPointsOfInterestSubLayerByQuery(Func<PrefabItemOptions, bool> query);

		PrefabItemOptions FindPointsofInterestSubLayerWithName(string poiLayerName);

		void RemovePointsOfInterestSubLayerWithName(string poiLayerName);

		void RemovePointsOfInterestSubLayer(PrefabItemOptions layer);

		#endregion

		#region Poi Api Methods

		/// <summary>
		/// Places a prefab at the specified LatLon on the Map.
		/// </summary>
		/// <param name="prefab"> A Game Object Prefab.</param>
		/// <param name="LatLon">A Vector2d(Latitude Longitude) object</param>
		void SpawnPrefabAtGeoLocation(GameObject prefab,
											 Vector2d LatLon,
											 Action<List<GameObject>> callback = null,
											 bool scaleDownWithWorld = true,
									  string locationItemName = "New Location");


		/// <summary>
		/// Places a prefab at all locations specified by the LatLon array.
		/// </summary>
		/// <param name="prefab"> A Game Object Prefab.</param>
		/// <param name="LatLon">A Vector2d(Latitude Longitude) object</param>
		void SpawnPrefabAtGeoLocation(GameObject prefab,
											 Vector2d[] LatLon,
											 Action<List<GameObject>> callback = null,
											 bool scaleDownWithWorld = true,
									  string locationItemName = "New Location");

		/// <summary>
		/// Places the prefab for supplied categories.
		/// </summary>
		/// <param name="prefab">GameObject Prefab</param>
		/// <param name="categories"><see cref="LocationPrefabCategories"/> For more than one category separate them by pipe
		/// (eg: LocationPrefabCategories.Food | LocationPrefabCategories.Nightlife)</param>
		/// <param name="density">Density controls the number of POIs on the map.(Integer value between 1 and 30)</param>
		/// <param name="locationItemName">Name of this location prefab item for future reference</param>
		/// <param name="scaleDownWithWorld">Should the prefab scale up/down along with the map game object?</param>
		void SpawnPrefabByCategory(GameObject prefab,
										   LocationPrefabCategories categories = LocationPrefabCategories.AnyCategory,
										   int density = 30, Action<List<GameObject>> callback = null,
										   bool scaleDownWithWorld = true,
								   string locationItemName = "New Location");


		/// <summary>
		/// Places the prefab at POI locations if its name contains the supplied string
		/// <param name="prefab">GameObject Prefab</param>
		/// <param name="nameString">This is the string that will be checked against the POI name to see if is contained in it, and ony those POIs will be spawned</param>
		/// <param name="density">Density (Integer value between 1 and 30)</param>
		/// <param name="locationItemName">Name of this location prefab item for future reference</param>
		/// <param name="scaleDownWithWorld">Should the prefab scale up/down along with the map game object?</param>
		/// </summary>
		void SpawnPrefabByName(GameObject prefab,
									  string nameString,
									  int density = 30,
									  Action<List<GameObject>> callback = null,
									  bool scaleDownWithWorld = true,
											  string locationItemName = "New Location");
		#endregion
	}
	
}