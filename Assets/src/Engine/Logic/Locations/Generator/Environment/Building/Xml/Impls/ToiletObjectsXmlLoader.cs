using Engine.Logic.Locations.Generator.Environment.Building.Impls;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls
{

    public class ToiletObjectsXmlLoader : RoomObjectsXmlLoader<ToiletItemType>
    {

        public override string[] FileNames => new[] { "Data/Rooms/toilet_environment" };

    }

}
