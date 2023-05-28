using System;
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
    [Serializable]
    public class CharacterLootGeneration : ICharacterLootGen
    {
        
        /// <summary>
        ///     Использовать ли список оружия как возможные (то-есть генерировать конечный список случайным образом
        ///     из предложенных), или интерпретировать список как строго обязательный?
        ///     ---
        ///     Use the list of weapons as possible (i.e., generate a final list at random, or
        ///     from the suggested ones), or interpret the list as strictly mandatory?
        /// </summary>
        public bool IsRandomWeaponsGeneration { get; set; }

        /// <summary>
        ///     Генерируемое оружие, которым враг будет пользоваться
        ///     ---
        ///     Generated weapons that the character will use
        /// </summary>
        public List<long> WeaponsForGeneration { get; set; }

        /// <summary>
        ///     Максимальное число генерируемого оружия
        ///     ---
        ///     Maximum number of weapons generated
        /// </summary>
        public int WeaponsMaxCountForGeneration { get; set; }

        
        /// <summary>
        ///     Использовать ли список предметов как возможные (то-есть генерировать конечный список случайным образом
        ///     из предложенных), или интерпретировать список как строго обязательный?
        ///     ---
        ///     Use the list of items as possible (i.e., generate a final list at random, or
        ///     from the suggested ones), or interpret the list as strictly mandatory?
        /// </summary>
        public bool IsRandomItemsGeneration { get; set; }
        
        /// <summary>
        ///     Генерируемые предметы находящиеся в сумках у врага
        ///     ---
        ///     Generated items found in character bags
        /// </summary>
        public List<ResourcePair> ItemsForGeneration { get; set; }

        /// <summary>
        ///     Максимальное число генерируемых предметов
        ///     ---
        ///     Maximum number of generated items
        /// </summary>
        public int ItemsMaxCountForGeneration { get; set; }

    }

}
