using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// Словарь объектов
    /// </summary>
    public partial class DataDictionary
    {

        public static class Items
        {
            
            public static ISet<long> SYSTEM_ITEMS = new HashSet<long>
            {
                Weapons.WEAPON_SYSTEM_HANDS,
                Weapons.WEAPON_SYSTEM_TOOTHS,
                Weapons.WEAPON_SYSTEM_ZOMBIE_HANDS
            };

            public static bool IsSystemHands(long id)
            {
                return id == Weapons.WEAPON_SYSTEM_ZOMBIE_HANDS || id == Weapons.WEAPON_SYSTEM_HANDS;
            }

        }

    }

}
