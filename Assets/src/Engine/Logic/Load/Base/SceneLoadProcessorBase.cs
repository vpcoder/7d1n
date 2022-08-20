using System.Collections;
using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Load
{

    public abstract class SceneLoadProcessorBase : Panel, ISceneLoadProcessor
    {

        [SerializeField] private Text txtDescription;
        [SerializeField] private Text txtTitle;

        protected const float MIN_WAIT = 0.001f;
        protected bool isLoaded;
        protected bool isLoadingProcess;

        private void Awake()
        {
            if(!isLoadingProcess && !isLoaded)
                StartCoroutine(LoadProcess());
        }
        
        public bool IsLoaded { get { return !isLoadingProcess && isLoaded; } }

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
            isLoadingProcess = true;
            Show();
            OnStartLoad();
        }

        public virtual void CompleteLoad()
        {
            Hide();
            isLoadingProcess = false;
            OnCompleteLoad();
        }

        public abstract void OnStartLoad();

        public abstract void OnCompleteLoad();

        public virtual IEnumerator LoadProcess()
        {
            StartLoad();
            yield return new WaitForSeconds(MIN_WAIT);
            CompleteLoad();
        }

    }

}
