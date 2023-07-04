
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Типы оружия, пистолеты, винтовки, биты, и т.д.
    /// Определяет тип анимации для данного оружия, когда оно находится в руках персонажей
    /// ---
    /// Types of weapons, pistols, rifles, bats, etc.
    /// Determines the type of animation for this weapon when it is in the hands of characters
    /// 
    /// </summary>
    public enum WeaponType : int
    {
        Custom       = 0x00,
        Hands        = 0x01,
        OneHanded    = 0x02,
        TwoHanded    = 0x03,
        Grenade      = 0x04,
        Pistol       = 0x05,
        Riffle       = 0x06,
        AssaultRifle = 0x07,
    };

}
