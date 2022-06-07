using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using UnityEngine;
using Random = System.Random;

namespace Engine.Logic.Locations.Generator.Furniture
{
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
    public abstract class FurnitureProcessorBase<E> : IFurnitureProcessor<E>
                                                where E : struct
    {

        #region Properties
        
        /// <summary>
        ///     Тип помещения для которого происходит генерация
        ///     ---
        ///     Type of room for which the generation takes place
        /// </summary>
        public abstract RoomKindType RoomType { get; }

        #endregion
        
        #region Shared Methods
        
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
        /// <param name="random">
        ///     Рандомизатор для текущего помещения, с учётом здания, этажа и комнаты
        ///     ---
        ///     Randomizer for the current room, taking into account building, floor and room
        /// </param>
        /// <returns>
        ///     Коллекцию сгенерированной мебели
        ///     ---
        ///     A collection of generated furniture
        /// </returns>
        protected abstract ICollection<IFurnitureItem<E>> CreateProxy(GenerationRoomContext context, Random random);

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
        public ICollection<IFurnitureItem<E>> CreateE(GenerationRoomContext context)
        {
            // Текущий сид на основе здания + этажа + комнаты
            var seed = (int) context.BuildInfo.BuildID + (int) context.BuildInfo.CurrentFloor + (int) context.RoomKindType;
            Debug.Log("Generation items for '" + context.RoomKindType + "', seed: '" + seed + "'...");
            var items = CreateProxy(context, new Random(seed));
            Debug.Log("Generated '" + items.Count + "' furniture item");
            return items;
        }

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
        public ICollection<IEnvironmentItem> Create(GenerationRoomContext context)
        {
            var items = CreateE(context);
            if (Lists.IsEmpty(items))
                return new List<IEnvironmentItem>();
            
            var factory = RoomSuperFactory.Instance.Get<E>(context.RoomKindType);
            if (factory == null)
                throw new NotSupportedException("factory for '" + typeof(E).FullName + "' not founded!");

            var availableItems = new List<IEnvironmentItem>();
            foreach (var item in items)
            {
                var environmentItem = factory.Get(item.Type);
                if (environmentItem == null)
                    throw new NotSupportedException("factory '" + typeof(E).FullName + "' isn't contains furniture '" + item.Type + "'!");
                
                for(int i = 0; i < item.Count; i++)
                    availableItems.Add(environmentItem);
            }

            return availableItems;
        }
        
        #endregion

        protected void AddItem(IList<IFurnitureItem<E>> list, E type, int count = 1)
        {
            if (count == 0)
                return;
            
            list.Add(new FurnitureItem<E>()
            {
                Type = type,
                Count = count,
            });
        }
        
    }
    
}