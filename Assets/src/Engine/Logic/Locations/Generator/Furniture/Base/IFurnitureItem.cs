namespace Engine.Logic.Locations.Generator.Furniture
{

    /// <summary>
    ///
    /// Сгенерированный объект мебели
    /// ---
    /// Generated furniture object
    /// 
    /// </summary>
    public interface IFurnitureItem
    {
        
        /// <summary>
        ///     Количество сгенерированных объектов мебели
        ///     ---
        ///     Number of generated furniture objects 
        /// </summary>
        int Count { get; set; }
        
    }
    
    /// <summary>
    ///
    /// Сгенерированный объект мебели
    /// ---
    /// Generated furniture object
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Енум всех возможных объектов мебели в помещении
    ///     ---
    ///     Enum of all possible furniture objects in the room
    /// </typeparam>
    public interface IFurnitureItem<E> : IFurnitureItem
                                    where E : struct
    {
        
        /// <summary>
        ///     Текущий объект мебели (значение енума)
        ///     ---
        ///     Current furniture object (enum value)
        /// </summary>
        E Type { get; set; }
        
    }
    
}