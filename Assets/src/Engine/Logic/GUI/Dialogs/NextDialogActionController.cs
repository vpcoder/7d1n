using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Dialog
{
    
    public class NextDialogActionController : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private DialogBox dialog;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            dialog.NextAction();
        }
        
    }
    
}
