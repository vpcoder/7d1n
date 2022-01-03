
namespace Engine.Data
{

    /// <summary>
    /// Огнестрельное оружие
    /// </summary>
    public interface IFirearmsWeapon : IWeapon
    {

        /// <summary>
        /// Размер магазина
        /// </summary>
        long AmmoStackSize { get; set; }

        /// <summary>
        /// Текущее количество патронов в магазине
        /// </summary>
        long AmmoCount { get; set; }

        /// <summary>
        /// Тип патронов
        /// </summary>
        long AmmoID { get; set; }

        /// <summary>
        /// Коэффициент пробития (от 0 до 100)
        /// </summary>
        byte Penetration { get; set; }

        /// <summary>
        /// Цена перезарядки
        /// </summary>
        int ReloadAP { get; set; }

        /// <summary>
        /// Как выглядит снаряд при выстреле
        /// </summary>
        string AmmoEffectType { get; set; }

        /// <summary>
        /// Эффект огня из ствола
        /// </summary>
        string ShootEffectType { get; set; }

        /// <summary>
        /// Звук выстрела
        /// </summary>
        string ShootSoundType { get; set; }

        /// <summary>
        /// Звук перезарядки
        /// </summary>
        string ReloadSoundType { get; set; }

    }

}
