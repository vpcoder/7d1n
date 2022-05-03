using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;

namespace Engine.Logic.Locations.Generator
{
    
    public struct TileSegmentLink
    {
        public static readonly TileSegmentLink Empty = new TileSegmentLink()
        {
            IsEmpty = true,
        };

        public bool IsEmpty { get; set; }
        public TileItem Tile { get; set; }
        public TileLayoutType Layout { get; set; }
        public EdgeLayout EdgeLayout { get; set; }
        public TileSegmentType SegmentType { get; set; }
        public EdgeType EdgeType { get; set; }
        public IEnvironmentItem Item { get; set; }
        public FloorMarker Marker { get; set; }
    }
    
}
