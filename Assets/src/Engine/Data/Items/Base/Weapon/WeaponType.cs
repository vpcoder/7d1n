using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Типы оружия, пистолеты, винтовки, биты, и т.д.
    /// ---
    /// Types of weapons, pistols, rifles, bats, etc.
    /// 
    /// </summary>
    public enum WeaponType : int
    {
        Custom       = 0x00,
        Hands        = 0x01,
        Knife        = 0x02,
        TwoHanded    = 0x03,
        Grenade      = 0x04,
        Pistol       = 0x05,
        Riffle       = 0x06,
        AssaultRifle = 0x07,
    };

}
