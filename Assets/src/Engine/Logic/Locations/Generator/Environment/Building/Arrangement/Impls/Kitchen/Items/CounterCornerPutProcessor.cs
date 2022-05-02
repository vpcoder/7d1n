using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class CounterCornerProcessor : ItemPutBaseProcessor<KitchenItemType>
    {
        
        public override KitchenItemType Type => KitchenItemType.CounterCorner;
        
        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var sinkList = Parent.ArrangementContext.GetItems(KitchenItemType.Sink);
            var sink = sinkList == null || sinkList.Count == 0 ? null : sinkList[0];
            if (sink == null)
                return false;

            var link = sink.Context;
            var cornerSegments = link.Tile.GetFurnitureOnTheLayoutByFindCrossDirection(TileLayoutType.Floor, link.SegmentType, 6, Filter);
            return TryPutOnRandomSegment(context, cornerSegments, EdgeLayout.Floor, currentInsertItem);
        }
        
        private bool Filter(TileSegmentLink link)
        {
            if (link.Item != null)
                return false; // Нас не интерисуют заполненные мебелью сегменты
            return link.Tile.HasInnerCorner; // Тайлы с внутренними углами
        }
        
    }
    
}