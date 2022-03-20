using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;
using System;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    /// <summary>
    /// 
    /// Фабрика для работы с объектами в жилом помещении "Туалет"
    /// ---
    /// Factory for working with objects in the living space "Toilet"
    /// 
    /// </summary>
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
