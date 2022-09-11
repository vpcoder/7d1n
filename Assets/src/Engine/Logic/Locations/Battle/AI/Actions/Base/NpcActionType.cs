
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Типы действий ИИ существ
    /// ---
    /// Types of AI creature actions
    /// 
    /// </summary>
    public enum NpcActionType : byte
    {

        /// <summary>
        ///     Пустое действие, ничего не делание
        ///     ---
        ///     Empty action, doing nothing
        /// </summary>
        None,

        /// <summary>
        ///     Действие ожидания, для замедления работы ИИ, чтобы существа вели себя более естественно
        ///     ---
        ///     Waiting action, to slow down the AI so that creatures behave more naturally
        /// </summary>
        Wait,

        /// <summary>
        ///     Действие перемещения
        ///     ---
        ///     Move Action
        /// </summary>
        Move,

        /// <summary>
        ///     Действие поворота в пространстве (осмотреться)
        ///     ---
        ///     The action of turning in space (look around)
        /// </summary>
        Look,

        /// <summary>
        ///     Действие атаки оружием на которое переключались ранее
        ///     ---
        ///     The attack action of the weapon you switched to earlier
        /// </summary>
        Attack,

        /// <summary>
        ///     Действие переключения оружия
        ///     ---
        ///     Switching Weapons Action
        /// </summary>
        PickWeapon,

        /// <summary>
        ///     Действие перезарядки оружия на которое переключались ранее
        ///     ---
        ///     The action of reloading a weapon that was switched to earlier
        /// </summary>
        Reload,

    };

}
