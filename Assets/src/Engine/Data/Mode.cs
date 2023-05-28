
namespace Engine.Data
{

    public enum Mode
    {

        /// <summary>
        ///     Режим переключения между сценами
        ///     ---
        ///     Switching between scenes mode
        /// </summary>
        Switch,

        /// <summary>
        ///     Режим GUI - инвентари, панельки и прочее
        ///     ---
        ///     GUI mode - inventory, dashboards, etc.
        /// </summary>
        GUI,

        Dialog,
        
        /// <summary>
        ///     Обычный игровой режим
        ///     ---
        ///     Normal game mode
        /// </summary>
        Game,

        /// <summary>
        ///     Режим битвы
        ///     ---
        ///     Battle mode
        /// </summary>
        Battle,

    };

}
