using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class CounterCornerPutProcessor : ItemPutBaseProcessor<KitchenItemType>
    {
        
        public override KitchenItemType Type => KitchenItemType.CounterCorner;
        
        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var cornerSegments = FindCornerSegments();
            return TryPutOnRandomSegment(context, cornerSegments, EdgeLayout.Floor, currentInsertItem);
        }

        // Поиск угловых точек от раковины
        private IList<TileSegmentLink> FindCornerSegments()
        {
            var sinkList = Parent.ArrangementContext.GetItems(KitchenItemType.Sink);
            var sink = Lists.First(sinkList);
            if (sink == null)
                return null;
            var link = sink.Context;
            var direction = TileService.AlongsideDirection(link.EdgeLayout); // Направление вдоль стены
            return TileService.GetFurnitureOnTheLayoutByFindBothDirection(link.Tile, TileLayoutType.Floor, link.SegmentType, direction, 6, Filter);
        }

        private bool Filter(TileSegmentLink link)
        {
            if (link.Item != null || link.Tile.HasDoor)
                return false; // Нас не интерисуют заполненные мебелью сегменты и сегменты у двери
            return link.Tile.InCorner(link.SegmentType); // Тайлы с внутренними углами
        }
        
    }
    
}