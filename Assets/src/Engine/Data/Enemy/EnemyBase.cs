using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// Базовый класс противника
    /// </summary>
    public abstract class EnemyBase : IEnemy
    {

        ///<summary>
        /// Идентификатор
        ///</summary>
        public long ID { get; set; }

        /// <summary>
        /// Текущие очки действия
        /// </summary>
        public int AP { get; set; }

        /// <summary>
        /// Группа-хода
        /// </summary>
        public EnemyGroup EnemyGroup { get; set; }

        /// <summary>
        /// Очков опыта
        /// </summary>
        public long Exp { get; set; }

        /// <summary>
        /// Общее здоровье
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Защита
        /// </summary>
        public int Protection { get; set; }

        /// <summary>
        /// Оружие в руках
        /// </summary>
        public List<IWeapon> Weapons { get; set; }

        /// <summary>
        /// Предметы в сумке
        /// </summary>
        public List<IItem> Items { get; set; }

        /// <summary>
        /// Генерируемое оружие, которым враг будет пользоваться
        /// </summary>
        public List<long> WeaponsForGeneration { get; set; }

        /// <summary>
        /// Максимальное число генерируемого оружия
        /// </summary>
        public int WeaponsMaxCountForGeneration { get; set; }

        /// <summary>
        /// Генерируемые предметы находящиеся в сумках у врага
        /// </summary>
        public List<Part> ItemsForGeneration { get; set; }

        /// <summary>
        /// Максимальное число генерируемых предметов
        /// </summary>
        public int ItemsMaxCountForGeneration { get; set; }

        public abstract IIdentity Copy();

    }

}
