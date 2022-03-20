using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    public class KitchenFactory : RoomFactoryBase<KitchenItemType, KitchenObjectsXmlLoader>
    {

        #region Singleton

        private static readonly Lazy<KitchenFactory> instance = new Lazy<KitchenFactory>(() => new KitchenFactory());
        public static KitchenFactory Instance { get { return instance.Value; } }
        private KitchenFactory()
        { }

        #endregion

    }

}
