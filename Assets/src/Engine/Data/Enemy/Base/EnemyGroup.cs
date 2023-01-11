
namespace Engine.Data
{

    public enum EnemyGroup : byte
    {

        /// <summary>
        ///     Реальные люди, игрок и его команда
        ///     ---
        ///     Real people, a player and his team
        /// </summary>
        PlayerGroup = 0x00,

        /// <summary>
        ///     Реальные люди, другая группа игроков
        ///     ---
        ///     Real people, another group of players
        /// </summary>
        AnotherPlayerGroup = 0x01,
        
        /// <summary>
        ///     Дикие животные
        ///     ---
        ///     Wild Animals
        /// </summary>
        WildAnimalsGroup = 0x02,
        
        /// <summary>
        ///     Зомби
        ///     ---
        ///     Zombies
        /// </summary>
        ZombieGroup = 0x03,
        
        /// <summary>
        ///     Усопшие
        ///     ---
        ///     
        /// </summary>
        DeceasedGroup = 0x04,
        
        /// <summary>
        ///     Мародеры
        ///     ---
        ///     
        /// </summary>
        MaraudersGroup = 0x05,
        
        /// <summary>
        ///     Скифы
        ///     ---
        ///     
        /// </summary>
        ScythiansGroup = 0x06,
        
        /// <summary>
        ///     Новый свет
        ///     ---
        ///     
        /// </summary>
        NewLightGroup = 0x07,
        
        /// <summary>
        ///     Технократы
        ///     ---
        ///     
        /// </summary>
        TechnocratsGroup = 0x08,
        
        /// <summary>
        ///     Спринтеры
        ///     ---
        ///     
        /// </summary>
        SprintersGroup = 0x09,
        
        /// <summary>
        ///     Реконструкторы
        ///     ---
        ///     
        /// </summary>
        ReconstructionistsGroup = 0x0A,
        
    };

}
