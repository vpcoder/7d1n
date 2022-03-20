using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    public class ToiletFactory : RoomFactoryBase<ToiletItemType, ToiletObjectsXmlLoader>
    {

        #region Singleton

        private static readonly Lazy<ToiletFactory> instance = new Lazy<ToiletFactory>(() => new ToiletFactory());
        public static ToiletFactory Instance { get { return instance.Value; } }
        private ToiletFactory()
        { }

        #endregion

    }

}
