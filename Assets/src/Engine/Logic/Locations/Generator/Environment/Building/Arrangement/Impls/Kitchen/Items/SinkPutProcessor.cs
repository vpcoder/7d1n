using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class SinkProcessor : ItemPutBaseProcessor<KitchenItemType>
    {

        public override KitchenItemType Type => KitchenItemType.Sink;

        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var onTheDoorNearWindow = new List<TileSegmentLink>();
            var tilesList = context.TilesInfo.TilesNearWindow;
            
            foreach (var floor in tilesList)
                onTheDoorNearWindow.AddRange(TileService.GetFurnitureOnTheLayoutByEdge(floor, TileLayoutType.Floor, EdgeType.Window, Filter));

            return TryPutOnRandomSegment(context, onTheDoorNearWindow, EdgeLayout.Floor, currentInsertItem);
        }

        private bool Filter(TileSegmentLink link)
        {
            if (link.Item != null // Не рассматриваем места которые уже заняты
                || link.Tile.HasDoor // Если рядом дверь - игнорируем такое место
                || link.Tile.InCorner(link.SegmentType)) // Не рассматриваем места в углах
                return false;

            // Все остальные места нам подходят
            return true;
        }
        
    }
    
}