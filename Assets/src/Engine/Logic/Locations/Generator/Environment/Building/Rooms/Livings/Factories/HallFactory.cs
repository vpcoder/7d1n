using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;
using System;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    /// <summary>
    /// 
    /// Фабрика для работы с объектами в жилом помещении "Зал"
    /// ---
    /// Factory for working with objects in the living space "Hall"
    /// 
    /// </summary>
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
