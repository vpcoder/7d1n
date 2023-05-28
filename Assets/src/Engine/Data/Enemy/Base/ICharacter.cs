using System.Collections.Generic;

namespace Engine.Data
{
    
    /// <summary>
    /// 
    /// Базовый класс персонажей
    /// ---
    /// Base characters class
    /// 
    /// </summary>
    public interface ICharacter : IIdentity
    {

        /// <summary>
        ///     Текущие очки действия (ОД)
        ///     ---
        ///     Current action points (AP)
        /// </summary>
        int AP { get; set; }

        /// <summary>
        ///     Группа-хода
        ///     ---
        ///     Order-group
        /// </summary>
        OrderGroup OrderGroup { get; set; }

        /// <summary>
        ///     Очков опыта, за убийство этого существа
        ///     ---
        ///     Experience points for killing this creature
        /// </summary>
        long Exp { get; set; }

        /// <summary>
        ///     Общее здоровье
        ///     ---
        ///     Overall health
        /// </summary>
        int Health { get; set; }

        /// <summary>
        ///     Защита
        ///     ---
        ///     Protection
        /// </summary>
        int Protection { get; set; }

        /// <summary>
        ///     Оружие в руках
        ///     ---
        ///     Weapon in hand
        /// </summary>
        List<IWeapon> Weapons { get; set; }

        /// <summary>
        ///     Предметы в сумке
        ///     ---
        ///     Items in the bag
        /// </summary>
        List<IItem> Items { get; set; }

        /// <summary>
        ///     Генерируемые предметы у NPC
        ///     ---
        ///     Generated items from NPCs
        /// </summary>
        public CharacterLootGeneration GenerationInfo { get; set; }

    }

}
