using Mapbox.Unity.MeshGeneration.Filters;

namespace Mapbox.Unity.SourceLayers
{
    
    public interface ILayerFilter
    {
        bool FilterKeyContains(string key);
        bool FilterKeyMatchesExact(string key);
        bool FilterUsesOperationType(LayerFilterOperationType layerFilterOperationType);
        bool FilterPropertyContains(string property);
        bool FilterPropertyMatchesExact(string property);
        bool FilterNumberValueEquals(float value);
        bool FilterNumberValueIsGreaterThan(float value);
        bool FilterNumberValueIsLessThan(float value);
        bool FilterIsInRangeValueContains(float value);

        string GetKey { get; }
        LayerFilterOperationType GetFilterOperationType { get; }

        string GetPropertyValue { get; }
        float GetNumberValue { get; }

        float GetMinValue { get; }
        float GetMaxValue { get; }

        void SetStringContains(string key, string property);
        void SetNumberIsEqual(string key, float value);
        void SetNumberIsLessThan(string key, float value);
        void SetNumberIsGreaterThan(string key, float value);
        void SetNumberIsInRange(string key, float min, float max);

    }
    
}