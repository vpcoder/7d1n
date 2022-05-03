using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class CounterCornerProcessor : ItemPutBaseProcessor<KitchenItemType>
    {
        
        public override KitchenItemType Type => KitchenItemType.CounterCorner;
        
        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var sinkList = Parent.ArrangementContext.GetItems(KitchenItemType.Sink);
            var sink = Lists.First(sinkList);
            if (sink == null)
                return false;

            var link = sink.Context;
            var cornerSegments = TileService.GetFurnitureOnTheLayoutByFindCrossDirection(link.Tile, TileLayoutType.Floor, link.SegmentType, 6, Filter);
            return TryPutOnRandomSegment(context, cornerSegments, EdgeLayout.Floor, currentInsertItem);
        }
        
        private bool Filter(TileSegmentLink link)
        {
            if (link.Item != null || link.Tile.HasDoor)
                return false; // Нас не интерисуют заполненные мебелью сегменты и сегменты у двери
            return link.Tile.InCorner(link.SegmentType); // Тайлы с внутренними углами
        }
        
    }
    
}