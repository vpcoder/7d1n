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
        Floor   = 0x00,
        Wall    = 0x01,
        Ceiling = 0x02,
    };
    
}