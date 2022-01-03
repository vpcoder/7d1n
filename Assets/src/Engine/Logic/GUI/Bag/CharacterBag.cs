using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic
{

    public enum ItemFilterType : int
    {
        All       = 0x00,
        Resources = 0x01,
        Cloths    = 0x02,
        Weapons   = 0x03,
        Tools     = 0x04,
        Medics    = 0x05,
        Builds    = 0x06,
    };

    public static class ItemFilterTypeAdditional
    {

        public static ItemFilterType GroupTypeToFilter(this GroupType type, IItem item)
        {
            if (item.ToolType != ToolType.None)
                return ItemFilterType.Tools;

            switch(type)
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

                case GroupType.LocationObject:
                    return ItemFilterType.Builds;

                default:
                    return ItemFilterType.All;
            }
        }

    }

    public class CharacterBag : Bag
    {

        public ItemFilterType Filter { get; set; } = ItemFilterType.All;
        
        public override IList<IItem> Items
        {
            get
            {
                if (Filter == ItemFilterType.All)
                    return base.Items;
                return base.Items.Where(item => item.Type.GroupTypeToFilter(item) == Filter).ToList();
            }
            set
            {
                base.Items = value;
            }
        }

        public override void Show()
        {
            base.Show();
            ObjectFinder.Find<InventoryActionPanelController>().Item = null;
            ObjectFinder.Find<GroundBag>().ScanGround();
            this.Items = Game.Instance.Character.Inventory.Items;
            Redraw();
        }

        public override void ClickItem(IItem item)
        {
            ObjectFinder.Find<InventoryActionPanelController>().Item = item;
        }

    }

}
