using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Load
{

    public abstract class SceneLoadProcessorBase : Panel, ISceneLoadProcessor
    {

        [SerializeField] private Text txtDescription;
        [SerializeField] private Text txtTitle;

        protected bool loadingFlag = true;

        public bool IsLoading { get { return loadingFlag; } }

        public void SetTitle(string title)
        {
            txtTitle.text = title;
        }

        public void SetDescription(string message)
        {
            txtDescription.text = message;
        }

        public virtual void StartLoad()
        {
            loadingFlag = true;
            Show();
            OnStartLoad();
        }

        public virtual void CompleteLoad()
        {
            Hide();
            loadingFlag = false;
            OnCompleteLoad();
        }

        public abstract void OnStartLoad();

        public abstract void OnCompleteLoad();

    }

}
