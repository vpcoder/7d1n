using System;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    public abstract class ItemBehaviour : MonoBehaviour, IItemBehaviour
    {

        [SerializeField] private ItemInfo itemInfo;

        public virtual IItem Item
        {
            get
            {
                return ItemSerializator.Convert(itemInfo);
            }
            set
            {
                itemInfo = ItemSerializator.Convert(value);
            }
        }

        public virtual ItemInfo ItemInfo
        {
            get
            {
                return itemInfo;
            }
            set
            {
                this.itemInfo = value;
            }
        }
    }

}
