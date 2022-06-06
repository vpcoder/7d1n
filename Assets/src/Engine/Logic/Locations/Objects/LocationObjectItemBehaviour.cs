using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Объект в локации.
    /// Объект связан с предметом по ИД, игрок может взаимодействовать с этим предметом, брать его в рюкзак, использовать (например, дверь)
    /// ---
    /// Object in the location.
    /// The object is linked to the object by ID, the player can interact with this object, take it in his backpack, use it (e.g. door)
    /// 
    /// </summary>
    public class LocationObjectItemBehaviour : MonoBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     Информация о предмете
        ///     ---
        ///     Item information
        /// </summary>
        [SerializeField] private ItemInfo itemInfo;
        
        #endregion
        
        #region Properties
        
        /// <summary>
        ///     Предмет с соответствующими сведениями
        ///     ---
        ///     The subject with the relevant information
        /// </summary>
        public IItem Item => ItemSerializator.Convert(ItemInfo);
        
        /// <summary>
        ///     Информация о предмете
        ///     ---
        ///     Item information
        /// </summary>
        public ItemInfo ItemInfo => itemInfo;
        
        #endregion

        #region Unity Events
        
        private void Start()
        {
            if(gameObject.GetComponent<ItemSelectController>() == null)
                gameObject.AddComponent<ItemSelectController>();
        }
        
        #endregion
        
    }

}
