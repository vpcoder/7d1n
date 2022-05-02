using Engine.Logic.Locations.Generator.Environment.Building.Impls;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls
{

    public class BedroomObjectsXmlLoader : RoomObjectsXmlLoader<BedroomItemType>
    {

        public override string[] FileNames => new[] { "Data/Rooms/bedroom_environment" };

    }

}
