using System.Collections.Generic;

namespace Engine.Data
{
    
    /// <summary>
    /// Противник
    /// </summary>
    public interface IEnemy : IIdentity
    {

        /// <summary>
        /// Текущие очки действия
        /// </summary>
        int AP { get; set; }

        /// <summary>
        /// Группа-хода
        /// </summary>
        EnemyGroup EnemyGroup { get; set; }

        /// <summary>
        /// Очков опыта
        /// </summary>
        long Exp { get; set; }

        /// <summary>
        /// Общее здоровье
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Защита
        /// </summary>
        int Protection { get; set; }

        /// <summary>
        /// Оружие в руках
        /// </summary>
        List<IWeapon> Weapons { get; set; }

        /// <summary>
        /// Предметы в сумке
        /// </summary>
        List<IItem> Items { get; set; }


        /// <summary>
        /// Генерируемое оружие, которым враг будет пользоваться
        /// </summary>
        List<long> WeaponsForGeneration { get; set; }

        /// <summary>
        /// Максимальное число генерируемого оружия
        /// </summary>
        int WeaponsMaxCountForGeneration { get; set; }

        /// <summary>
        /// Генерируемые предметы находящиеся в сумках у врага
        /// </summary>
        List<ResourcePair> ItemsForGeneration { get; set; }

        /// <summary>
        /// Максимальное число генерируемых предметов
        /// </summary>
        int ItemsMaxCountForGeneration { get; set; }

    }

}
