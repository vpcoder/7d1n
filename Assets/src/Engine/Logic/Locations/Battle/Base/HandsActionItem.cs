using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    public enum HandActionType : int
    {
        AttackFirearms,
        ReloadFirearms,
        AttackEdged,
        ThrowEdged,
        ThrowGrenade,
    };

    public class HandsActionItem : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        [SerializeField] private Image image;
        [SerializeField] private HandsActionsController actionsController;

        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.green;

        [SerializeField] public HandActionType Action;

        public void Show()
        {
            body.SetActive(true);
        }

        public void Hide()
        {
            body.SetActive(false);
        }

        public void DoSelect()
        {
            image.color = selectedColor;
        }

        public void DoUnselect()
        {
            image.color = normalColor;
        }


        public void Click()
        {
            actionsController.Select(this);
        }

    }

}
