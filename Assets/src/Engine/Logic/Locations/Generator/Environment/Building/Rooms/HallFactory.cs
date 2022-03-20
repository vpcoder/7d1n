using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    public class HallFactory : RoomFactoryBase<HallItemType, HallObjectsXmlLoader>
    {

        #region Singleton

        private static readonly Lazy<HallFactory> instance = new Lazy<HallFactory>(() => new HallFactory());
        public static HallFactory Instance { get { return instance.Value; } }
        private HallFactory()
        { }

        #endregion

    }

}
