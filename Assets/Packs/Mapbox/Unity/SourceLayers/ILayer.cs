
namespace Mapbox.Unity.Map
{
	
	public interface ILayer
	{
		/// <summary>
		/// Gets the type of feature from the `FEATURES` section.
		/// </summary>
		MapLayerType LayerType { get; }
		/// <summary>
		/// Boolean for setting the feature layer active or inactive.
		/// </summary>
		bool IsLayerActive { get; }
		/// <summary>
		/// Gets the source ID for the feature layer.
		/// </summary>
		string LayerSourceId { get; }

		/// <summary>
		/// Gets the `Data Source` for the `MAP LAYERS` section.
		/// </summary>
		void SetLayerSource(string source);
		void Initialize();
		void Initialize(LayerProperties properties);
		void Update(LayerProperties properties);
		void Remove();
	}
	
}
