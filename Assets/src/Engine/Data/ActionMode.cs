
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Текущий режим действий игрока
    /// ---
    /// Current player action mode
    /// 
    /// </summary>
    public enum ActionMode
    {

        /// <summary>
        ///     Игрок хочет выполнять перемещение персонажа по локации
        ///     ---
        ///     The player wants to move the character around the location
        /// </summary>
        Move,

        /// <summary>
        ///     Игрок хочет выполнять прицеливание из оружия
        ///     ---
        ///     The player wants to perform gun aiming
        /// </summary>
        Aim,

        /// <summary>
        ///     Игрок хочет управлять камерой
        ///     ---
        ///     The player wants to control the camera
        /// </summary>
        Rotation,

    }

}
