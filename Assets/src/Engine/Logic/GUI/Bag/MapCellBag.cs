using Engine.Data;
using UnityEngine;

namespace Engine.Logic
{

    public class MapCellBag : Bag
    {

        private LocationCellData data;
        public LocationCellData Data
        {
            get
            {
                return data;
            }
            set
            {
                this.data = value;

                Items.Clear();

                if (value == null)
                {
                    if(Visible)
                        Redraw();
                    return;
                }

                foreach(ItemInfo info in Data.Items)
                {
                    var item = ItemSerializator.Convert(info);
                    Items.Add(item);
                }

                if (Visible)
                    Redraw();
            }
        }

        public override void Show()
        {
            base.Show();
            Redraw();
        }

        public override void ClickItem(IItem item)
        {
            Game.Instance.Character.Inventory.Add(item);
            Items.Remove(item);
            Redraw();
            ObjectFinder.Find<CharacterBag>().Redraw();
        }

    }

}
