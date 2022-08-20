
namespace Engine.Data
{

    public enum EnemyGroup : byte
    {

        /// <summary>
        /// Реальные люди, игрок и его команда
        /// </summary>
        PlayerGroup = 0x00,

        /// <summary>
        /// Реальные люди, другая группа игроков
        /// </summary>
        AnotherPlayerGroup = 0x01,

        /// <summary>
        /// НПС, зомби
        /// </summary>
        ZombieGroup = 0x02,

        /// <summary>
        /// НПС, обычно союзник
        /// </summary>
        AlliedGroup = 0x03,

        /// <summary>
        /// НПС, обычно враждебные
        /// </summary>
        HostilesGroup = 0x04,

    };

}
