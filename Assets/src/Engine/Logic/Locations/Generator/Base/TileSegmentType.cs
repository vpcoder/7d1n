namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    ///
    /// Сегменты маленьких тайлов на полу
    /// ---
    /// Segments of small tiles on the floor
    /// 
    /// </summary>
    public enum TileSegmentType : int
    {
        
        /// <summary>
        /// -----------
        /// |  * |    |
        /// |----|----|
        /// |    |    |
        /// -----------
        /// </summary>
        S00,
        
        /// <summary>
        /// -----------
        /// |    |  * |
        /// |----|----|
        /// |    |    |
        /// -----------
        /// </summary>
        S01,
        
        /// <summary>
        /// -----------
        /// |    |    |
        /// |----|----|
        /// |  * |    |
        /// -----------
        /// </summary>
        S10,
        
        /// <summary>
        /// -----------
        /// |    |    |
        /// |----|----|
        /// |    |  * |
        /// -----------
        /// </summary>
        S11,
        
    };
    
}