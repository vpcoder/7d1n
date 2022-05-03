using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Furniture
{
    
    public class KitchenFurnitureProcessor : FurnitureProcessorBase<KitchenItemType>
    {
        
        public override RoomKindType RoomType => RoomKindType.Kitchen;

        protected override ICollection<IFurnitureItem<KitchenItemType>> CreateProxy(GenerationRoomContext context, Random random)
        {
            var items = new List<IFurnitureItem<KitchenItemType>>();

            // Добавляем раковину
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.Sink,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.CounterWashing,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.Oven,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.CounterCorner,
                Count = 2,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.Counter,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.EmptyCounter,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.Fridge,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.Table,
                Count = 1,
            });
            
            items.Add(new FurnitureItem<KitchenItemType>()
            {
                Type = KitchenItemType.Chair,
                Count = 3,
            });
            
            return items;
        }

    }
    
}