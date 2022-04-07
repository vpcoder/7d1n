namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    ///
    /// Тип "грани"
    /// У тайла пола есть 4 грани, по краям которых может находится что то из этого списка
    /// ---
    /// The "faces" type
    /// The floor has 4 faces, on the edges of which there can be something from this list
    /// 
    /// </summary>
    public enum EdgeType : int
    {
        
        /// <summary>
        ///     Что то пошло не так?
        ///     ---
        ///     Did something go wrong?
        /// </summary>
        Empty,
        
        /// <summary>
        ///     На этой грани глухая стена
        ///     ---
        ///     There is a blank wall on this edge
        /// </summary>
        Wall,
        
        /// <summary>
        ///     На этой грани оконный проём
        ///     ---
        ///     On this edge is a window opening
        /// </summary>
        Window,
        
        /// <summary>
        ///     На этой грани дверной проём
        ///     ---
        ///     On this facet of the doorway
        /// </summary>
        Door,
        
    };
    
}