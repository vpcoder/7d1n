namespace Engine.Logic.Locations.Animation
{

    /// <summary>
    /// 
    /// Получение персонажем урона.
    /// ---
    /// Taking damage to a character.
    /// 
    /// </summary>
    public enum TakeDamageType : int
    {

        /// <summary>
        ///     Ничего не происходит, дополнительной анимации нет
        ///     ---
        ///     Nothing happens, there is no additional animation
        /// </summary>
        Empty = 0x00,

        /// <summary>
        ///     Персонаж получил урон, 1 сет анимации урона
        ///     ---
        ///     Character received damage, 1 set of damage animation
        /// </summary>
        Take1 = 0x01,

    };

}
