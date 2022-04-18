using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen
{

    public class Kitchen01ArrangementProcessor : ArrangementProcessorBase<KitchenItemType>
    {

        public override RoomKindType RoomType => RoomKindType.Kitchen;
        
        public override bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            foreach (var item in context.TilesInfo.TilesData)
            {
                item.Marker.Emission = Color.black;
                item.Marker.Segments.Clear();
            }

            if (currentInsertItem.Type == KitchenItemType.Sink)
            {
                var onTheDoorNearWindow = new List<TileSegmentLink>();
                var tilesList = context.TilesInfo.TilesNearWindow;
            
                foreach (var floor in tilesList)
                    onTheDoorNearWindow.AddRange(floor.GetFurnitureOnTheFloorCloseToWindow());

                var list = onTheDoorNearWindow.Where(link => link.Item == null).ToList();
                if (list.Count != 0)
                {
                    var index = context.RoomRandom.Next(0, list.Count - 1);
                    var item = list[index];
                    item.Tile.Set(item.Layout, item.SegmentType, currentInsertItem);
                    item.Marker.Segments[item.SegmentType] = Color.magenta;
                    Object.Instantiate(currentInsertItem.ToObject, item.Marker.GetSegmentPos(EdgeLayout.Floor, item.SegmentType), item.Marker.GetLayoutRot(item.EdgeLayout), BuildParent);
                }
            }

            return true;
        }
        
    }

}
