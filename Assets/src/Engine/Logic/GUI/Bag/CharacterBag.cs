using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic
{

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
