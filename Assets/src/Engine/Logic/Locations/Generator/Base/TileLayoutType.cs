namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    ///
    /// Слой, на котором располагается мебель
    /// Пол, Стена, потолок
    /// ---
    /// The layer on which the furniture is placed
    /// Floor, Wall, Ceiling
    /// 
    /// </summary>
    public enum TileLayoutType : int
    {
        
        /// <summary>
        ///     Слой расположен на полу
        ///     ---
        ///     The layer is located on the floor
        /// </summary>
        Floor   = 0x00,
        
        /// <summary>
        ///     Слой расположен на одной из стен
        ///     ---
        ///     The layer is located on one of the walls
        /// </summary>
        WallLeft    = 0x01,
        WallRight   = 0x02,
        WallTop     = 0x03,
        WallBottom  = 0x04,
        
        /// <summary>
        ///     Слой расположен на потолке
        ///     ---
        ///     The layer is located on the ceiling
        /// </summary>
        Ceiling = 0x05,
        
    };
    
}