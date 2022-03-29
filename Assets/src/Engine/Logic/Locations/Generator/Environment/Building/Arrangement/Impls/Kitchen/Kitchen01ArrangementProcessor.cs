using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen
{

    public class Kitchen01ArrangementProcessor : ArrangementProcessorBase<KitchenItemType>
    {

        public override RoomKindType RoomType => RoomKindType.Kitchen;


        public override bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {

            


            return true;
        }

    }

}
