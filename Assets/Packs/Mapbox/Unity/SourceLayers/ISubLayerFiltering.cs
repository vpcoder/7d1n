using System.Collections.Generic;
using Mapbox.Unity.MeshGeneration.Filters;

namespace Mapbox.Unity.SourceLayers
{
    
    public interface ISubLayerFiltering
    {
        ILayerFilter AddStringFilterContains(string key, string property);
        ILayerFilter AddNumericFilterEquals(string key, float value);
        ILayerFilter AddNumericFilterLessThan(string key, float value);
        ILayerFilter AddNumericFilterGreaterThan(string key, float value);
        ILayerFilter AddNumericFilterInRange(string key, float min, float max);

        ILayerFilter GetFilter(int index);

        void RemoveFilter(int index);
        void RemoveFilter(LayerFilter filter);
        void RemoveFilter(ILayerFilter filter);
        void RemoveAllFilters();

        IEnumerable<ILayerFilter> GetAllFilters();
        IEnumerable<ILayerFilter> GetFiltersByQuery(System.Func<ILayerFilter, bool> query);

        LayerFilterCombinerOperationType GetFilterCombinerType();

        void SetFilterCombinerType(LayerFilterCombinerOperationType layerFilterCombinerOperationType);
    }
    
}