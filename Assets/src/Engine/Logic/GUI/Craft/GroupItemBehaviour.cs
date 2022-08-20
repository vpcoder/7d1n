using Engine.Data;
using UnityEngine;

namespace Engine.Logic
{
    
    public class GroupItemBehaviour : MonoBehaviour
    {
        
        #region Hidden Fields
        
        [SerializeField] private BlueprintGroupType group;

        #endregion
        
        #region Properties
        
        public BlueprintGroupType Group => group;
        
        #endregion

        public void OnClick()
        {
            ObjectFinder.Find<CraftController>().OnGroupClick(this);
        }
        
    }
    
}