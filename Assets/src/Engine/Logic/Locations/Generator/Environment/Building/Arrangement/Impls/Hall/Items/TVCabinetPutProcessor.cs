using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Hall.Items
{
    
    public class TVCabinetProcessor : ItemPutBaseProcessor<HallItemType>
    {

        public override HallItemType Type => HallItemType.TVCabinet;

        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<HallItemType> currentInsertItem)
        {
            var onTheDoorNearWindow = new List<TileSegmentLink>();
            var tilesList = context.TilesInfo.TilesNearWall;
            
            foreach (var floor in tilesList)
                onTheDoorNearWindow.AddRange(TileService.GetFurnitureOnTheLayoutByEdge(floor, TileLayoutType.Floor, EdgeType.Wall, Filter));

            return TryPutOnRandomSegment(context, onTheDoorNearWindow, EdgeLayout.Floor, currentInsertItem, true);
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