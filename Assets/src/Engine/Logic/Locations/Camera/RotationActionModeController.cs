using Engine.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Location
{

    public class RotationActionModeController : MonoBehaviour
    {

        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Image imgButton;

        public ActionMode PrevActionMode
        {
            get;
            set;
        }

        private bool IsRotation
        {
            get
            {
                return Game.Instance.Runtime.ActionMode == ActionMode.Rotation;
            }
        }

        public void Switch()
        {
            if (IsRotation)
            {
                Game.Instance.Runtime.ActionMode = PrevActionMode;
            }
            else
            {
                PrevActionMode = Game.Instance.Runtime.ActionMode;
                Game.Instance.Runtime.ActionMode = ActionMode.Rotation;
            }
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            imgButton.color = IsRotation ? selectedColor : normalColor;
        }

    }

}
