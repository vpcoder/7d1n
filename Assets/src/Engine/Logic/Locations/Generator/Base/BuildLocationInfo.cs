
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
        ///     Количество комнат на этажах
        ///     ---
        ///     Number of rooms on the floors
        /// </summary>
        public long RoomsCount { get; set; }



    }

}
