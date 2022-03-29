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
        
        /// <summary>
        ///     Комната - Спальня
        ///     ---
        ///     Room - Bedroom
        /// </summary>
        public override RoomKindType RoomType => RoomKindType.Bedroom;
        
    }

}
