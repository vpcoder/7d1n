using System;

namespace Engine.Data
{

    /// <summary>
    /// Базовый класс оружия
    /// </summary>
    [Serializable]
    public abstract class Weapon : CraftableItem, IWeapon
    {

        /// <summary>
        /// Урон от оружия
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Радиус прицеливания
        /// </summary>
        public float AimRadius { get; set; }

        /// <summary>
        /// Максимальная дистанция атаки оружием
        /// </summary>
        public long MaxDistance { get; set; }

        /// <summary>
        /// Цена использования ОД
        /// </summary>
        public int UseAP { get; set; }

    }

}
