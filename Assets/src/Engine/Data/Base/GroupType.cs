
namespace Engine.Data
{

    ///<summary>
    /// Группы предметов и обьектов
    ///</summary>
    public enum GroupType : byte
    {

        Item            = 0x00,
        Resource        = 0x01,
        Used            = 0x02, // Еда, аптечки, расходники

        ClothHead       = 0x10,
        ClothBody       = 0x11,
        ClothHand       = 0x12,
        ClothLegs       = 0x13,
        ClothBoot       = 0x14,
        
        WeaponFirearms  = 0x20, // Огнестрел
        WeaponEdged     = 0x21, // Холодное оружие
        WeaponGrenade   = 0x22, // Гранаты

        Ammo            = 0x24, // Патроны и снаряды

        LocationObject  = 0xf0,

    };
	
}

