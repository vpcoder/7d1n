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

        private Vector3 playerEyePos;
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
                if (playerCharacter == null)
                    playerCharacter = ObjectFinder.Character.gameObject;
                return playerCharacter;
            }
        }

        public Vector3 PlayerEyePos => playerEyePos;

        private void Start()
        {
            LoadFactory.Instance.Complete += Init;
        }

        public virtual void Init() { }
        
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
            playerEyePos = ObjectFinder.Character.Eye.position;
            
            var dialogBox = ObjectFinder.DialogBox;
            dialogBox.Runtime.StartEvent += StartDialogEvent;
            dialogBox.Runtime.EndEvent   += EndDialogEvent;

            var mainDialog = new DialogQueue();
            StartDialogProcessing(mainDialog);
            CreateDialog(mainDialog);

            mainDialog.Run(() =>
            {
                foreach (var runtimeObject in mainDialog.RuntimeObjectList)
                {
                    if(runtimeObject != null)
                        runtimeObject.Destruct();
                }
            });
            mainDialog.Run(() =>
            {
                StoryActionHelper.Fade(ObjectFinder.SceneViewImage, Color.white, Color.clear, 
                    0.8f); // Не добавляем RuntimeObjectList, чтобы скрипт доиграл до конца гарантированно
            });
            
            var endDialog = new DialogQueue();
            EndDialogProcessing(endDialog);
            
            dialogBox.SetDialogQueueAndRun(mainDialog.Queue, endDialog.Queue, 0, this);
        }
        
    }
    
}