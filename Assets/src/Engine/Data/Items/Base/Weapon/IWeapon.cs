
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
        /// Максимальная дистанция атаки оружием в игровых метрах
        /// ---
        /// Maximum attack distance with a weapon in game meters
        /// </summary>
        long MaxDistance { get; set; }

        /// <summary>
        /// Урон от оружия
        /// ---
        /// Weapon damage
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Цена использования ОД
        /// ---
        /// The price of using AP
        /// </summary>
        int UseAP { get; set; }

    }

}
