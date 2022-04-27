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
                onTheDoorNearWindow.AddRange(floor.GetEmptyFurnitureOnTheLayoutByEdge(TileLayoutType.Floor, EdgeType.Window));

            return TryPutOnRandomSegment(context, onTheDoorNearWindow, EdgeLayout.Floor, currentInsertItem);
        }
        
    }
    
}