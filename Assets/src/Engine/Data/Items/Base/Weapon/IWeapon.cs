
namespace Engine.Data
{

    /// <summary>
    /// Оружие
    /// </summary>
    public interface IWeapon : ICraftableItem
    {

        /// <summary>
        /// Максимальная дистанция атаки оружием
        /// </summary>
        long MaxDistance { get; set; }

        /// <summary>
        /// Урон от оружия
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Цена использования ОД
        /// </summary>
        int UseAP { get; set; }

    }

}
