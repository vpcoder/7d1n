using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;
using System;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    /// <summary>
    /// 
    /// Фабрика для работы с объектами в жилом помещении "Кухня"
    /// ---
    /// Factory for working with objects in the living space "Kitchen"
    /// 
    /// </summary>
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
