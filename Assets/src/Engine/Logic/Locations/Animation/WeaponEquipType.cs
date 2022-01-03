
namespace Engine.Logic.Locations.Animation
{

    /// <summary>
    /// Виды оружия в руках человека
    /// </summary>
    public enum WeaponEquipType : int
    {

        /// <summary>
        /// В руках ничего нет
        /// </summary>
        Empty         = 0x00,

        /// <summary>
        /// В руках одноручное оружие ближнего боя
        /// </summary>
        EdgedOneHand  = 0x01,

        /// <summary>
        /// В руках двуручное оружие ближнего боя
        /// </summary>
        EdgedTwoHand  = 0x02,

        /// <summary>
        /// В руках пистолет
        /// </summary>
        Pistol        = 0x03,

        /// <summary>
        /// В руках винтовка
        /// </summary>
        Riffle        = 0x04,

        /// <summary>
        /// В руках штурмовая винтовка
        /// </summary>
        AssaultRiffle = 0x05,

        /// <summary>
        /// Граната
        /// </summary>
        Grenade       = 0x06,

    };

}
