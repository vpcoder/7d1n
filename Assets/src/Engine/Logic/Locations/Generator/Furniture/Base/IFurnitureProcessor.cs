using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;

namespace Engine.Logic.Locations.Generator.Furniture
{

    /// <summary>
    /// 
    /// Процессор генерации мебели для помещений
    /// ---
    /// Room furniture generation processor
    /// 
    /// </summary>
    public interface IFurnitureProcessor
    {
        
        /// <summary>
        ///     Тип помещения для которого происходит генерация
        ///     ---
        ///     Type of room for which the generation takes place
        /// </summary>
        RoomKindType RoomType { get; }

        /// <summary>
        ///     Формирует коллекцию мебели в помещении
        ///     ---
        ///     Forms a collection of furniture in the room
        /// </summary>
        /// <returns>
        ///     Коллекцию сгенерированной мебели
        ///     ---
        ///     A collection of generated furniture
        /// </returns>
        ICollection<IEnvironmentItem> Create(GenerationRoomContext context);
        
    }

    /// <summary>
    /// 
    /// Процессор генерации мебели для помещений
    /// ---
    /// Room furniture generation processor
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Енум со всеми возможными типами мебели для помещения
    ///     ---
    ///     Enum with all possible types of room furniture
    /// </typeparam>
    public interface IFurnitureProcessor<E> : IFurnitureProcessor
                                             where E : struct
    {
        
        /// <summary>
        ///     Формирует коллекцию мебели в помещении
        ///     ---
        ///     Forms a collection of furniture in the room
        /// </summary>
        /// <param name="context">
        ///     Контекст генерируемого помещения
        ///     ---
        ///     Context of the generated room
        /// </param>
        /// <returns>
        ///     Коллекцию сгенерированной мебели
        ///     ---
        ///     A collection of generated furniture
        /// </returns>
        ICollection<IFurnitureItem<E>> CreateE(GenerationRoomContext context);

    }
    
}