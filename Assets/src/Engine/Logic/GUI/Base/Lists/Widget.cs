using UnityEngine;

namespace Engine.Logic.Base
{
    public abstract class Widget : MonoBehaviour, IWidget
    {
        public virtual void Show()
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("construct for '" + GetType().Name + "'...");
#endif

            ShowConstruct();

#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("show...");
#endif

            Body.SetActive(true);
        }

        public virtual void Hide()
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("destruct for '" + GetType().Name + "'...");
#endif

            HideDestruct();

#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("hide...");
#endif

            Body.SetActive(false);
        }

        #region Fields

        [SerializeField] private bool visible;

        [SerializeField] private GameObject body;

        #endregion

        #region Properties

        public bool Visible
        {
            get => visible;
            set
            {
                if (visible == value)
                    return;

                visible = value;

                if (visible)
                    Show();
                else
                    Hide();
            }
        }

        public GameObject Body => body;

        public RectTransform Rect => (RectTransform)transform;

        #endregion

        #region Virtual

        protected virtual void ShowConstruct()
        {
        }

        protected virtual void HideDestruct()
        {
        }

        #endregion
    }
}