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

            AddItem(items, KitchenItemType.Sink);
            
            AddItem(items, KitchenItemType.CounterWashing);
            AddItem(items, KitchenItemType.Oven);
            AddItem(items, KitchenItemType.Extractor);
            
            AddItem(items, KitchenItemType.CounterCorner, random.Next(1, 2));
            AddItem(items, KitchenItemType.Counter, random.Next(1, 2));
            AddItem(items, KitchenItemType.EmptyCounter);
            AddItem(items, KitchenItemType.Fridge);
            AddItem(items, KitchenItemType.Table);
            
            AddItem(items, KitchenItemType.Chair, random.Next(1, 4));
            
            AddItem(items, KitchenItemType.Toaster);
            AddItem(items, KitchenItemType.Microwave);

            return items;
        }

    }
    
}