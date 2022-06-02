using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class LocationObjectItemBehaviour : MonoBehaviour
    {

        [SerializeField] private long itemID;

        public long ID
        {
            get
            {
                return itemID;
            }
        }

        public IItem Item
        {
            get
            {
                return ItemFactory.Instance.Get(itemID);
            }
        }

    }

}
