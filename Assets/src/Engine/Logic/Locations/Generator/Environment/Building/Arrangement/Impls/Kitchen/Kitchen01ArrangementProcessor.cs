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
            foreach (var item in context.TilesInfo.TilesData.Where(o => o.HasWindow).Select(o => o.Marker))
            {
                Debug.Log(item.ToObject.name);
            }


            return true;
        }

    }

}
