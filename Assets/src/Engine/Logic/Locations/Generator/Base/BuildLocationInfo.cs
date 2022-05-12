using Engine.Generator;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Информация о здании в целом
    /// Здание, этажи, планировка комнат
    /// ---
    /// Information about the building as a whole
    /// Building, floors, room layouts
    /// 
    /// </summary>
    public class BuildLocationInfo
    {

        /// <summary>
        ///     Идентификатор здания
        ///     ---
        ///     Building identifier
        /// </summary>
        public long BuildID { get; set; }

        /// <summary>
        ///     Количество этажей в здании
        ///     ---
        ///     Number of floors in the building
        /// </summary>
        public long FloorsCount { get; set; }

        /// <summary>
        ///     Текущий этаж
        ///     ---
        ///     Current floor
        /// </summary>
        public long CurrentFloor { get; set; }

        /// <summary>
        ///     Тип локации (тип здания)
        ///     ---
        ///     Location type (building type)
        /// </summary>
        public LocationType LocationType { get; set; }

        /// <summary>
        ///     Общий стиль комнат в здании
        ///     ---
        ///     The general style of the rooms in the building
        /// </summary>
        public RoomType RoomType { get; set; }

        /// <summary>
        ///     Информация о NPC в помещениях
        ///     ---
        ///     Information about NPCs in rooms
        /// </summary>
        public BuildEnemyInfo EnemyInfo { get; set; }

    }

}
