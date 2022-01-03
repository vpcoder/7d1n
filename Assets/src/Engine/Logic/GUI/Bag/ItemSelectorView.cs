using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic
{

    public class ItemSelectorView : Bag
    {

        public CharacterClothCell Selected { get; set; }

        public override IList<IItem> Items
        {
            get
            {
                if(Selected.IsWeapon)
                    return base.Items.Where(item => item.Type == GroupType.WeaponEdged
                    || item.Type == GroupType.WeaponFirearms 
                    || item.Type == GroupType.WeaponGrenade).ToList();

                return base.Items.Where(item => item.Type == Selected.Type).ToList();
            }
            set
            {
                base.Items = value;
            }
        }

        public override void Show()
        {
            base.Show();
            this.Items = Game.Instance.Character.Inventory.Items;
            Redraw();
        }

        public override void ClickItem(IItem item)
        {
            Selected.DoEquip(item);
            Selected.HideButtons();
            Hide();
        }

        public void OnCancelClick()
        {
            Selected.HideButtons();
            Hide();
        }

        public void OnDropClick()
        {
            Selected.DoEquip(null);
            Selected.HideButtons();
            Hide();
        }

    }

}
