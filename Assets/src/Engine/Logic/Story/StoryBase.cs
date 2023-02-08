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
        [SerializeField] private bool destroyStoryObjectOnEnd = true;

        private GameObject topPanel;
        private GameObject playerCharacter;

        public GameObject TopPanel
        {
            get
            {
                if (topPanel == null)
                    topPanel = ObjectFinder.TopPanel;
                return topPanel;
            }
        }

        public GameObject PlayerCharacter
        {
            get
            {
                if(playerCharacter == null)
                    playerCharacter = ObjectFinder.Find<LocationCharacter>().gameObject;
                return playerCharacter;
            }
        }

        private void Start()
        {
            LoadFactory.Instance.Complete += Init;
        }

        public virtual void Init()
        { }
        
        public abstract void CreateDialog(DialogQueue dlg);

        protected virtual void StartDialogProcessing(DialogQueue dlg) { }
        
        protected virtual void EndDialogProcessing(DialogQueue dlg) { }

        protected virtual void EndDialogEvent()
        {
            if(hidePlayer)
                PlayerCharacter.SetActive(true);
            
            if(hideTopPanel)
                TopPanel.SetActive(true);
            
            if (destroyStoryObjectOnEnd)
                Destroy(gameObject);
        }

        protected virtual void StartDialogEvent()
        {
            if (hidePlayer)
                PlayerCharacter.SetActive(false);
            
            if(hideTopPanel)
                TopPanel.SetActive(false);
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