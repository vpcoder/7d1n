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
        S00 = 0x00,
        
        /// <summary>
        /// -----------
        /// |    |    |
        /// |----|----|
        /// |  * |    |
        /// -----------
        /// </summary>
        S01 = 0x01,
        
        /// <summary>
        /// -----------
        /// |    |  * |
        /// |----|----|
        /// |    |    |
        /// -----------
        /// </summary>
        S10 = 0x02,
        
        /// <summary>
        /// -----------
        /// |    |    |
        /// |----|----|
        /// |    |  * |
        /// -----------
        /// </summary>
        S11 = 0x03,
        
    };
    
}