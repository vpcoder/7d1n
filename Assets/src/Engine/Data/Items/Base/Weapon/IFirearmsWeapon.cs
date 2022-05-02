
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Огнестрельное оружие, дистанционное оружие, луки, арбалеты и т.д.
    /// ---
    /// Firearms, remote weapons, bows, crossbows, etc.
    /// 
    /// </summary>
    public interface IFirearmsWeapon : IWeapon
    {

        /// <summary>
        ///     Размер магазина оружия (максимальное число патронов в обойме)
        ///     ---
        ///     The size of the gun magazine (maximum number of rounds in a clip)
        /// </summary>
        long AmmoStackSize { get; set; }

        /// <summary>
        ///     Текущее количество патронов в магазине
        ///     ---
        ///     Current number of cartridges in the magazine
        /// </summary>
        long AmmoCount { get; set; }

        /// <summary>
        ///     Тип патронов, подходящих для этого оружия
        ///     ---
        ///     The type of ammunition suitable for this weapon
        /// </summary>
        long AmmoID { get; set; }

        /// <summary>
        ///     Коэффициент пробития объектов (от 0 до 100)
        ///     ---
        ///     Object penetration rate (0 to 100)
        /// </summary>
        byte Penetration { get; set; }

        /// <summary>
        ///     Цена перезарядки ОД
        ///     ---
        ///     The cost of reloading AP
        /// </summary>
        int ReloadAP { get; set; }

        /// <summary>
        ///     Как выглядит снаряд при выстреле
        ///     ---
        ///     What the projectile looks like when fired
        /// </summary>
        string AmmoEffectType { get; set; }

        /// <summary>
        ///     Эффект огня из ствола
        ///     ---
        ///     The effect of a gunshot
        /// </summary>
        string ShootEffectType { get; set; }

        /// <summary>
        ///     Звук выстрела
        ///     ---
        ///     The sound of a gunshot
        /// </summary>
        string ShootSoundType { get; set; }

        /// <summary>
        ///     Звук перезарядки
        ///     ---
        ///     Reload sound
        /// </summary>
        string ReloadSoundType { get; set; }

        /// <summary>
        ///     Звук клина оружия
        ///     ---
        ///     Sound of jamming weapons
        /// </summary>
        string JammingSoundType { get; set; }

    }

}
