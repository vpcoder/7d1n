using System.Collections.Generic;
using Engine.Data;

namespace Engine.Logic
{
    public enum ItemFilterType
    {
        All,
        Resources,
        Cloths,
        Weapons,
        Tools,
        Medics,
        Foods,
        Used,
        Builds,
    };

    public static class ItemFilterTypeAdditional
    {
        public static ItemFilterType GroupTypeToFilter(this GroupType type, IItem item)
        {
            if (Sets.IsNotEmpty(item.ToolType))
                return ItemFilterType.Tools;

            switch (type)
            {
                case GroupType.Resource:
                    return ItemFilterType.Resources;

                case GroupType.ClothHead:
                case GroupType.ClothBody:
                case GroupType.ClothHand:
                case GroupType.ClothLegs:
                case GroupType.ClothBoot:
                    return ItemFilterType.Cloths;

                case GroupType.WeaponGrenade:
                case GroupType.WeaponEdged:
                case GroupType.WeaponFirearms:
                case GroupType.Ammo:
                    return ItemFilterType.Weapons;

                case GroupType.Food:
                    return ItemFilterType.Foods;
                case GroupType.MedKit:
                    return ItemFilterType.Medics;
                case GroupType.Used:
                    return ItemFilterType.Used;

                case GroupType.LocationObject:
                    return ItemFilterType.Builds;

                default:
                    return ItemFilterType.All;
            }
        }
    }
}