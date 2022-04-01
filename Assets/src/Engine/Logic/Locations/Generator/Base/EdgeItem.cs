using Engine.Logic.Locations.Generator.Markers;


namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    ///
    /// Грань тайла на полу.
    /// То, с чем граничит тайл на этой грани.
    /// ---
    /// The edge of the tile on the floor.
    /// What is the border of the tile on this edge.
    /// 
    /// </summary>
    public struct EdgeItem
    {

        /// <summary>
        ///     
        /// </summary>
        public static EdgeItem Empty = new EdgeItem() { EdgeType = EdgeType.Empty, Marker = null };
        
        /// <summary>
        ///     Тип грани
        ///     ---
        ///     Edge Type
        /// </summary>
        public EdgeType EdgeType { get; set; }
        
        /// <summary>
        ///     Маркер того с кем граничит данная грань
        ///     ---
        ///     A marker of who the boundary is
        /// </summary>
        public IMarker Marker { get; set; }
        
    }
    
}