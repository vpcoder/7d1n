using Engine.Logic.Locations.Generator.Environment.Building.Xml.Impls;
using System;

namespace Engine.Logic.Locations.Generator.Environment.Building.Rooms
{

    /// <summary>
    /// 
    /// Фабрика для работы с объектами в жилом помещении "Спальня"
    /// ---
    /// Factory for working with objects in the living space "Badroom"
    /// 
    /// </summary>
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
