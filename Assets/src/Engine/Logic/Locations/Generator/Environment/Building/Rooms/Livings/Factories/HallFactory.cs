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

        /// <summary>
        ///     Комната - Зал
        ///     ---
        ///     Room - Hall
        /// </summary>
        public override RoomKindType RoomType => RoomKindType.Hall;

    }

}
