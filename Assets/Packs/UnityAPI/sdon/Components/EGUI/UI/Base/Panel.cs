using UnityEngine;

namespace Engine.EGUI
{

    public class Panel : MonoBehaviour, IPanel
    {

        [SerializeField] private GameObject body;
        [SerializeField] private bool visible = false;

        public GameObject Body => body;

        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible == value)
                    return;

                if (visible)
                    Show();
                else
                    Hide();
            }
        }
        public virtual void Hide()
        {
            if (!visible)
                return;
            visible = false;
            Body.SetActive(visible);
        }

        public virtual void Show()
        {
            if (visible)
                return;
            visible = true;
            Body.SetActive(visible);
        }

    }

}
