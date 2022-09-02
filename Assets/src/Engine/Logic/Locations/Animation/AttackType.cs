namespace Engine.Logic.Locations.Animation
{

    /// <summary>
    /// 
    /// Тип атаки. Определяет анимацию для каждого типа атаки.
    /// ---
    /// Attack Type. Specifies the animation for each type of attack.
    /// 
    /// </summary>
    public enum AttackType : int
    {

        /// <summary>
        ///     Ничего не происходит, дополнительной анимации нет
        ///     ---
        ///     Nothing happens, there is no additional animation
        /// </summary>
        Empty = 0x00,

        /// <summary>
        ///     Одиночный выстрел, происходит анимация отдачи
        ///     ---
        ///     Single shot, recoil animation occurs
        /// </summary>
        SingleShot = 0x01,

        /// <summary>
        ///     Стрельба очередью, происходит анимация отдачи
        ///     ---
        ///     Firing in bursts, there is a recoil animation
        /// </summary>
        BurstFiring = 0x02,

        /// <summary>
        ///     Перезарядка, происходит анимация смены обоймы
        ///     ---
        ///     Reloading, a clip change animation takes place
        /// </summary>
        Reload = 0x03,

        /// <summary>
        ///     Атака в ближнем бою, происходит анимация махания оружием
        ///     ---
        ///     Attack in close combat, there is an animation of waving a weapon
        /// </summary>
        EdgedAttack = 0x04,

        /// <summary>
        ///     Атака метанием оружия ближнего боя, происходит анимация замаха при броске
        ///     ---
        ///     Melee weapon throwing attack, swings animation occurs when throwing
        /// </summary>
        EdgedThrow = 0x05,

        /// <summary>
        ///     Атака гранатой, происходит анимация замаха при броске
        ///     ---
        ///     Grenade attack, swing animation occurs when thrown
        /// </summary>
        GrenadeThrow = 0x06,

        /// <summary>
        ///     Атака голыми руками, происходит анимация махания кулаками
        ///     ---
        ///     Attack with bare hands, there is an animation of waving fists
        /// </summary>
        HandsAttack = 0x0f,

    };

}
