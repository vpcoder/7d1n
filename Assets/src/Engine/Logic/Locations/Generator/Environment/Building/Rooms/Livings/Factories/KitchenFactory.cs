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

        /// <summary>
        ///     Комната - Кухня
        ///     ---
        ///     Room - Kitchen
        /// </summary>
        public override RoomKindType RoomType => RoomKindType.Kitchen;

    }

}
