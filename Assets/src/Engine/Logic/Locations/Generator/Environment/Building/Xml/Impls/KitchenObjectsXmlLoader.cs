using Engine.Logic.Locations.Generator.Environment.Building.Impls;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls
{

    public class KitchenObjectsXmlLoader : RoomObjectsXmlLoader<KitchenItemType>
    {

        public override string[] FileNames => new[] { "Data/Rooms/kitchen_environment" };

    }

}
