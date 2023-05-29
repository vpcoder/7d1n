
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

        /// <summary>
        ///     Игрок сейчас просматривает диалог или запущен режим кат-сцены
        ///     ---
        ///     The player is currently viewing a dialog or a cutscenes mode is running
        /// </summary>
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
