namespace Engine.Logic.Locations.Animation
{

    /// <summary>
    /// Тип атаки
    /// </summary>
    public enum AttackType : int
    {

        /// <summary>
        /// Ничего
        /// </summary>
        Empty = 0x00,

        /// <summary>
        /// Одиночный выстрел
        /// </summary>
        SingleShot = 0x01,

        /// <summary>
        /// Стрельба очередью
        /// </summary>
        BurstFiring = 0x02,

        /// <summary>
        /// Перезарядка
        /// </summary>
        Reload = 0x03,

    };

}
