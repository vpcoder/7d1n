using System;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Furniture
{
    
    public class HallFurnitureProcessor : FurnitureProcessorBase<HallItemType>
    {
        
        public override RoomKindType RoomType => RoomKindType.Hall;

        protected override ICollection<IFurnitureItem<HallItemType>> CreateProxy(GenerationRoomContext context, Random random)
        {
            var items = new List<IFurnitureItem<HallItemType>>();
            
            AddItem(items, HallItemType.TVCabinet);
            AddItem(items, HallItemType.TVSet, random.Next(0, 1));
            AddItem(items, HallItemType.Couch);
            AddItem(items, HallItemType.Chair, random.Next(0, 2));
            
            return items;
        }

    }
    
}