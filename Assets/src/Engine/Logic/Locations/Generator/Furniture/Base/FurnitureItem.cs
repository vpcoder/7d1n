namespace Engine.Logic.Locations.Generator.Furniture
{

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
    public struct FurnitureItem<E> : IFurnitureItem<E>
                                where E : struct
    {
        
        /// <summary>
        ///     Текущий объект мебели (значение енума)
        ///     ---
        ///     Current furniture object (enum value)
        /// </summary>
        public E Type { get; set; }

        /// <summary>
        ///     Количество сгенерированных объектов мебели
        ///     ---
        ///     Number of generated furniture objects 
        /// </summary>
        public int Count { get; set; }
        
    }
    
}