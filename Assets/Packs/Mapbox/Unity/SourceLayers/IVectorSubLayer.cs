using Mapbox.Unity.Map;
using Mapbox.Unity.SourceLayers;

namespace Mapbox.Unity.SourceLayers
{
    
    public interface IVectorSubLayer
    {
        /// <summary>
        /// Gets `Filters` data from the feature.
        /// </summary>
        ISubLayerFiltering Filtering { get; }
        /// <summary>
        /// Gets `Modeling` data from the feature.
        /// </summary>
        ISubLayerModeling Modeling { get; }
        /// <summary>
        /// Gets `Texturing` data from the feature.
        /// </summary>
        ISubLayerTexturing Texturing { get; }
        /// <summary>
        /// Gets `Behavior Modifiers` data from the feature.
        /// </summary>
        ISubLayerBehaviorModifiers BehaviorModifiers { get; }
    }
    
}