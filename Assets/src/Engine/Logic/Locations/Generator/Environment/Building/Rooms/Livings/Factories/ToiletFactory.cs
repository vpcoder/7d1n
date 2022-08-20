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

        /// <summary>
        ///     Комната - Туалет
        ///     ---
        ///     Room - Toilet
        /// </summary>
        public override RoomKindType RoomType => RoomKindType.Toilet;

    }

}
