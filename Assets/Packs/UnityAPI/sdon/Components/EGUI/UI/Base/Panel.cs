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
            if (visible == false)
                return;
            visible = false;
            Body.SetActive(visible);
        }

        public virtual void Show()
        {
            if (visible == true)
                return;
            visible = true;
            Body.SetActive(visible);
        }

    }

}
