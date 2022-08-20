using System.Collections.Generic;
using System.Linq;
using Engine.Data;

namespace Engine.Logic
{

    public class ItemSelectorView : Bag
    {

        public CharacterClothCell SelectedEquipCell { get; set; }

        public override IList<IItem> Items
        {
            get
            {
                if(SelectedEquipCell.IsWeapon)
                    return base.Items.Where(item => item.Type == GroupType.WeaponEdged
                    || item.Type == GroupType.WeaponFirearms 
                    || item.Type == GroupType.WeaponGrenade).ToList();

                return base.Items.Where(item => item.Type == SelectedEquipCell.Type).ToList();
            }
            set
            {
                base.Items = value;
            }
        }

        public override void Show()
        {
            base.Show();
            Items = Game.Instance.Character.Inventory.Items;
            Redraw();
        }

        public override void ClickItem(IItem item)
        {
            SelectedEquipCell.DoEquip(item);
            SelectedEquipCell.HideButtons();
            Hide();
        }

        public void OnCancelClick()
        {
            SelectedEquipCell.HideButtons();
            Hide();
        }

        public void OnDropClick()
        {
            SelectedEquipCell.DoEquip(null);
            SelectedEquipCell.HideButtons();
            Hide();
        }

    }

}
