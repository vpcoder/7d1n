using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    public class BedroomFactory : RoomFactoryBase<BedroomItemType, BedroomObjectsXmlLoader>
    {

        #region Singleton

        private static readonly Lazy<BedroomFactory> instance = new Lazy<BedroomFactory>(() => new BedroomFactory());
        public static BedroomFactory Instance { get { return instance.Value; } }
        private BedroomFactory()
        { }

        #endregion

    }

}
