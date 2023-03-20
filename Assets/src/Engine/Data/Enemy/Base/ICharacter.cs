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
        ///     Генерируемое оружие, которым враг будет пользоваться
        ///     ---
        ///     Generated weapons that the character will use
        /// </summary>
        List<long> WeaponsForGeneration { get; set; }

        /// <summary>
        ///     Максимальное число генерируемого оружия
        ///     ---
        ///     Maximum number of weapons generated
        /// </summary>
        int WeaponsMaxCountForGeneration { get; set; }

        /// <summary>
        ///     Генерируемые предметы находящиеся в сумках у врага
        ///     ---
        ///     Generated items found in character bags
        /// </summary>
        List<ResourcePair> ItemsForGeneration { get; set; }

        /// <summary>
        ///     Максимальное число генерируемых предметов
        ///     ---
        ///     Maximum number of generated items
        /// </summary>
        int ItemsMaxCountForGeneration { get; set; }

    }

}
