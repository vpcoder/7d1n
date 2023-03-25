using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Dialog
{
    
    public class NextDialogActionController : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private DialogBox dialog;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(dialog.IsAnswerBoxShowed)
                return;
            
            var action = dialog.Runtime.CurrentAction;
            if(action == null || action.WaitTime > 0f)
                return;
            
            // Щёлкаем, чтобы игрок комфортно воспринемал перелистывание
            // Click to make the player feel comfortable flipping
            AudioController.Instance.PlaySound("ui/skip");
            
            // Приступаем к следующему действию в диалоге
            // Proceed to the next action in the dialog
            dialog.NextAction();
        }
        
    }
    
}
