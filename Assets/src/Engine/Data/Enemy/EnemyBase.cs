using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Базовый класс противника
    /// ---
    /// Base enemy class
    /// 
    /// </summary>
    public abstract class EnemyBase : IEnemy
    {

        ///<summary>
        ///     Идентификатор
        ///     ---
        ///     Identifier
        ///</summary>
        public long ID { get; set; }

        /// <summary>
        ///     Текущие очки действия (ОД)
        ///     ---
        ///     Current action points (AP)
        /// </summary>
        public int AP { get; set; }

        /// <summary>
        ///     Группа-хода
        ///     ---
        ///     Order-group
        /// </summary>
        public EnemyGroup EnemyGroup { get; set; }

        /// <summary>
        ///     Очков опыта, за убийство этого существа
        ///     ---
        ///     Experience points for killing this creature
        /// </summary>
        public long Exp { get; set; }

        /// <summary>
        ///     Общее здоровье
        ///     ---
        ///     Overall health
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        ///     Защита
        ///     ---
        ///     Protection
        /// </summary>
        public int Protection { get; set; }

        /// <summary>
        ///     Оружие в руках
        ///     ---
        ///     Weapon in hand
        /// </summary>
        public List<IWeapon> Weapons { get; set; }

        /// <summary>
        ///     Предметы в сумке
        ///     ---
        ///     Items in the bag
        /// </summary>
        public List<IItem> Items { get; set; }

        /// <summary>
        ///     Генерируемое оружие, которым враг будет пользоваться
        ///     ---
        ///     Generated weapons that the enemy will use
        /// </summary>
        public List<long> WeaponsForGeneration { get; set; }

        /// <summary>
        ///     Максимальное число генерируемого оружия
        ///     ---
        ///     Maximum number of weapons generated
        /// </summary>
        public int WeaponsMaxCountForGeneration { get; set; }

        /// <summary>
        ///     Генерируемые предметы находящиеся в сумках у врага
        ///     ---
        ///     Generated items found in enemy bags
        /// </summary>
        public List<ResourcePair> ItemsForGeneration { get; set; }

        /// <summary>
        ///     Максимальное число генерируемых предметов
        ///     ---
        ///     Maximum number of generated items
        /// </summary>
        public int ItemsMaxCountForGeneration { get; set; }

        /// <summary>
        ///     Выполняет копирование текущего экземпляра врага
        ///     ---
        ///     Performs a copy of the current enemy instance
        /// </summary>
        /// <returns>
        ///     Копию текущего экземпляра врага, со всеми параметрами
        ///     ---
        ///     A copy of the current enemy instance, with all parameters
        /// </returns>
        public abstract IIdentity Copy();

    }

}
