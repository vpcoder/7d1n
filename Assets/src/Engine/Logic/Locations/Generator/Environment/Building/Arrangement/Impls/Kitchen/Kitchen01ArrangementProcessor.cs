using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen
{

    public class Kitchen01ArrangementProcessor : ArrangementProcessorBase<KitchenItemType>
    {

        public override LivingRoomKindType RoomType => LivingRoomKindType.Kitchen;


        public override bool InsertItemIntoScene(GenerationBuildContext contex, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {




            return true;
        }

    }

}
