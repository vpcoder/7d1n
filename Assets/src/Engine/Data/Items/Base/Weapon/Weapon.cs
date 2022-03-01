using System;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Базовый класс оружия
    /// ---
    /// Basic Weapon Class
    /// 
    /// </summary>
    [Serializable]
    public abstract class Weapon : CraftableItem, IWeapon
    {

        /// <summary>
        ///     Тип оружия
        ///     ---
        ///     Type of weapon
        /// </summary>
        public WeaponType WeaponType { get; set; }

        /// <summary>
        ///     Урон от оружия
        ///     ---
        ///     Weapon damage
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        ///     Радиус прицеливания в метрах
        ///     ---
        ///     Aiming radius in meters
        /// </summary>
        public float AimRadius { get; set; }

        /// <summary>
        ///     Максимальная дистанция атаки оружием
        ///     ---
        ///     Maximum attack distance with a weapon
        /// </summary>
        public float MaxDistance { get; set; }

        /// <summary>
        ///     Цена использования ОД
        ///     ---
        ///     The price of using APs
        /// </summary>
        public int UseAP { get; set; }

    }

}
