
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Оружие - то чем персонаж может атаковать
    /// ---
    /// Weapon - what the character can attack with
    /// 
    /// </summary>
    public interface IWeapon : ICraftableItem
    {

        /// <summary>
        ///     Тип оружия
        ///     ---
        ///     Type of weapon
        /// </summary>
        WeaponType WeaponType { get; set; }

        /// <summary>
        ///     Максимальная дистанция атаки оружием в игровых метрах
        ///     ---
        ///     Maximum attack distance with a weapon in game meters
        /// </summary>
        float MaxDistance { get; set; }

        /// <summary>
        ///     Радиус прицеливания при использование этого оружия (в игровых метрах)
        ///     ---
        ///     Aiming radius when using this weapon (in game meters)
        /// </summary>
        float AimRadius { get; set; }

        /// <summary>
        ///     Урон от оружия
        ///     ---
        ///     Weapon damage
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        ///     Цена использования ОД
        ///     ---
        ///     The price of using AP
        /// </summary>
        int UseAP { get; set; }

    }

}
