using System;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Поведение предмета на локации
    /// ---
    /// Item behavior on location
    /// 
    /// </summary>
    public abstract class ItemBehaviour : MonoBehaviour, IItemBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     Полные сведения о предмете
        ///     ---
        ///     Complete information about the item
        /// </summary>
        [SerializeField] private ItemInfo itemInfo;

        #endregion

        #region Properties

        /// <summary>
        ///     Экземпляр предмета полученный на основе полных сведений о предмете
        ///     ---
        ///     A copy of the item obtained on the basis of complete information about the item
        /// </summary>
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

        /// <summary>
        ///     Полные сведения о предмете
        ///     ---
        ///     Complete information about the item
        /// </summary>
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

        #endregion

    }

}
