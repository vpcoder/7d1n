
namespace Engine.Logic.Locations.Generator.Environment.Building
{

    /// <summary>
    /// 
    /// Известные типы помещений при генерации локаций
    /// ---
    /// Known room types when generating locations
    /// 
    /// </summary>
    public enum RoomKindType : int
    {

        /// <summary>
        ///     Коридоры, соединяющие комнаты
        ///     ---
        ///     Corridors connecting the rooms
        /// </summary>
        Corridor,
        
        /// <summary>
        ///     Кухня и её окружение
        ///     ---
        ///     The kitchen and its surroundings
        /// </summary>
        Kitchen,

        /// <summary>
        ///     Зал и его окружение
        ///     ---
        ///     The hall and its surroundings
        /// </summary>
        Hall,

        /// <summary>
        ///     Спальня и её окружение
        ///     ---
        ///     The bedroom and its surroundings
        /// </summary>
        Bedroom,

        /// <summary>
        ///     Туалет с ванной и окружение
        ///     ---
        ///     Toilet and tub and surroundings
        /// </summary>
        Toilet,

    };

}
