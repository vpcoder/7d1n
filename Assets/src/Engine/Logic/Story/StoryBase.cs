using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using src.Engine.Scenes.Loader;
using UnityEngine;

namespace Engine.Story
{
    
    public abstract class StoryBase : MonoBehaviour, IStory
    {

        [SerializeField] private bool hidePlayer;
        [SerializeField] private bool hideTopPanel = true;
        [SerializeField] private bool destroyOnEnd = true;

        private GameObject topPanel;
        private GameObject playerCharacter;
        
        private void Start()
        {
            LoadFactory.Instance.Complete += Init;
        }

        public virtual void Init()
        {
            topPanel = ObjectFinder.TopPanel;
            playerCharacter = ObjectFinder.Find<LocationCharacter>().gameObject;
        }
        
        public abstract void CreateDialog(DialogQueue dlg);

        protected virtual void StartDialogProcessing(DialogQueue dlg) { }
        
        protected virtual void EndDialogProcessing(DialogQueue dlg) { }

        protected virtual void EndDialogEvent()
        {
            if(hidePlayer)
                playerCharacter.SetActive(true);
            
            if(hideTopPanel)
                topPanel.SetActive(true);
            
            if (destroyOnEnd)
                Destroy(gameObject);
        }

        protected virtual void StartDialogEvent()
        {
            if(hidePlayer)
                playerCharacter.SetActive(false);
            
            if(hideTopPanel)
                topPanel.SetActive(false);
        }
        
        public void RunDialog()
        {
            var dialogBox = ObjectFinder.DialogBox;
            dialogBox.Runtime.StartEvent += StartDialogEvent;
            dialogBox.Runtime.EndEvent   += EndDialogEvent;

            var queue = new DialogQueue();
            StartDialogProcessing(queue);
            CreateDialog(queue);
            EndDialogProcessing(queue);
            
            dialogBox.SetDialogQueueAndRun(queue.Queue, 0, this);
        }
        
    }
    
}