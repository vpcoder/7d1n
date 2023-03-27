namespace Engine.Logic.Locations.Animation
{

    /// <summary>
    /// 
    /// Анимация смерти. Определяет возможные значения свойства смерти.
    /// ---
    /// Death animation. Defines the possible values of the death property.
    /// 
    /// </summary>
    public enum DeatType : int
    {

        /// <summary>
        ///     Персонаж жив.
        ///     ---
        ///     The character is alive.
        /// </summary>
        Alive = 0x00,

        /// <summary>
        ///     Персонаж по настоящему умер
        ///     ---
        ///     The character is really dead.
        /// </summary>
        Dead = 0x01,

        /// <summary>
        ///     Фейковая смерть, персонаж выглядит как мёртвый
        ///     ---
        ///     Fake death, the character looks dead
        /// </summary>
        FakeDead = 0x02,

    };

}
