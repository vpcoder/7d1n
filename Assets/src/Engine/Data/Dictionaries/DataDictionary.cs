using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Словарь объектов
    /// ---
    /// Object Dictionary
    /// 
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
                return id == Weapons.WEAPON_SYSTEM_ZOMBIE_HANDS 
                       || id == Weapons.WEAPON_SYSTEM_HANDS;
            }

            public static bool IsNotSystemHands(long id)
            {
                return !IsSystemHands(id);
            }
            
            public static bool IsSystemItem(long id)
            {
                return SYSTEM_ITEMS.Contains(id);
            }

            public static bool IsNotSystemItem(long id)
            {
                return !IsSystemItem(id);
            }

        }

    }

}
