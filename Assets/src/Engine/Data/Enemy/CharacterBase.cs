using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Базовый класс противника
    /// ---
    /// Base character class
    /// 
    /// </summary>
    public abstract class CharacterBase : ICharacter
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
        public OrderGroup OrderGroup { get; set; }

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
        ///     Генерируемые предметы у NPC
        ///     ---
        ///     Generated items from NPCs
        /// </summary>
        public CharacterLootGeneration GenerationInfo { get; set; }

        /// <summary>
        ///     Выполняет копирование текущего экземпляра врага
        ///     ---
        ///     Performs a copy of the current character instance
        /// </summary>
        /// <returns>
        ///     Копию текущего экземпляра врага, со всеми параметрами
        ///     ---
        ///     A copy of the current character instance, with all parameters
        /// </returns>
        public abstract IIdentity Copy();

    }

}
