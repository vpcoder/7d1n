using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class FridgePutProcessor : ItemPutBaseProcessor<KitchenItemType>
    {
        
        public override KitchenItemType Type => KitchenItemType.Fridge;
        
        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var foundedSegments = FindSegmentsBySink();
            if (Lists.IsEmpty(foundedSegments)) // Ничего не нашли
                foundedSegments = FindSegmentsByCorners();
            return TryPutOnRandomSegment(context, foundedSegments, EdgeLayout.Floor, currentInsertItem);
        }

        // Ищем свободные сегменты вдоль раковины
        private IList<TileSegmentLink> FindSegmentsBySink()
        {
            var sinkList = Parent.ArrangementContext.GetItems(KitchenItemType.Sink);
            var sink = Lists.First(sinkList);
            if (sink == null)
                return null;
            var link = sink.Context;
            var direction = TileService.AlongsideDirection(link.EdgeLayout); // Направление вдоль стены
            return TileService.GetFurnitureOnTheLayoutByFindBothDirection(link.Tile, TileLayoutType.Floor, link.SegmentType, direction, 6, Filter);
        }

        // Ищем продолжение кухонной столешницы по углам кухни
        private IList<TileSegmentLink> FindSegmentsByCorners()
        {
            var cornerList = FindCornerSegmentsBySink();
            if (Lists.IsEmpty(cornerList))
                return null;
            foreach (var cornerLink in cornerList)
            {
                var list = TileService.GetFurnitureOnTheLayoutByFindCorner(cornerLink, 6, Filter);
                if(Lists.IsEmpty(list))
                    continue;
                return Lists.newArrayList(Lists.First(list, TileSegmentLink.Empty));
            }
            return null;
        }
        
        // Поиск угловых точек от раковины
        private IList<TileSegmentLink> FindCornerSegmentsBySink()
        {
            var sinkList = Parent.ArrangementContext.GetItems(KitchenItemType.Sink);
            var sink = Lists.First(sinkList);
            if (sink == null)
                return null;
            var link = sink.Context;
            var direction = TileService.AlongsideDirection(link.EdgeLayout); // Направление вдоль стены
            return TileService.GetFurnitureOnTheLayoutByFindBothDirection(link.Tile, TileLayoutType.Floor, link.SegmentType, direction, 6, linkItem => linkItem.Tile.InCorner(linkItem.SegmentType) && !linkItem.Tile.HasDoor);
        }

        private bool Filter(TileSegmentLink link)
        {
            if (link.Item != null || link.Tile.HasDoor || link.Tile.HasWindow)
                return false; // Нас не интерисуют заполненные мебелью сегменты и сегменты у двери или окон
            // Остальные места нам подходят
            return true;
        }
        
    }
    
}