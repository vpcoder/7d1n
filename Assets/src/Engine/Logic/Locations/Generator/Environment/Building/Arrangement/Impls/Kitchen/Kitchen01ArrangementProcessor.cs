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
            }
            
            foreach (var item in context.TilesInfo.TilesNearWall)
            {
                item.Marker.Emission = Color.cyan;
            }
            
            foreach (var item in context.TilesInfo.TilesNearWindow)
            {
                item.Marker.Emission = Color.green;
            }
            
            foreach (var item in context.TilesInfo.TilesNearDoor)
            {
                item.Marker.Emission = Color.white;
            }


            return true;
        }

        
    }

}
