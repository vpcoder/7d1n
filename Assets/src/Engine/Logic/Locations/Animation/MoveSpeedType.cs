
namespace Engine.Logic.Locations.Animation
{

    /// <summary>
    /// Анимация перемещения
    /// </summary>
    public enum MoveSpeedType : int
    {

        /// <summary>
        /// Стояние на месте
        /// </summary>
        Idle   = 0x00,

        /// <summary>
        /// Шаг
        /// </summary>
        Walk   = 0x01,

        /// <summary>
        /// Быстрое перемещение
        /// </summary>
        Run    = 0x02,

        /// <summary>
        /// Быстрый бег
        /// </summary>
        Sprint = 0x03,

    };

}
