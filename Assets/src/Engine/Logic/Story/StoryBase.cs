using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Dialog;
using src.Engine.Scenes.Loader;
using UnityEngine;

namespace Engine.Story
{
    
    public abstract class StoryBase : MonoBehaviour, IStory
    {

        [SerializeField] protected bool activeFlag = true;
        [SerializeField] protected bool needRunOnStartFlag = false;
        
        [SerializeField] private bool useShowFade = true;
        [SerializeField] private bool hidePlayer;
        [SerializeField] private bool hideTopPanel = true;
        [SerializeField] private bool destroyGameObject = true;

        [SerializeField] private List<StoryBase> nextStories;
        
        private Vector3 playerEyePos;
        private GameObject topPanel;
        private GameObject playerCharacter;
        private StoryDataRepoObject storyInfo;

        public abstract string StoryID { get; }

        public virtual bool IsActive
        {
            get { return activeFlag; }
            set { activeFlag = value; }
        }

        public void SetActiveAndSave()
        {
            IsActive = true;
            QuestFactory.Instance.UpdateStoryInfo();
        }
        
        public virtual bool IsNeedRunOnStart
        {
            get { return needRunOnStartFlag; }
            set { needRunOnStartFlag = value; }
        }
        
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
            storyInfo = QuestFactory.Instance.GetStoryInfo(this);
            IsActive = storyInfo.IsActive;
        }

        public virtual void Init()
        {
            var canRun = storyInfo.IsComplete ? SecondInit() : FirstInit();
            if(canRun && IsNeedRunOnStart)
                RunDialog();
            
            if(!canRun)
                Complete();
        }

        public virtual void Complete()
        {
            if(storyInfo.IsComplete)
                SecondComplete();
            else
                FirstComplete();

            storyInfo.IsComplete = true;
            QuestFactory.Instance.UpdateStoryInfo();
            
            Destruct();
        }
        
        public abstract void CreateDialog(DialogQueue dlg);

        public virtual bool FirstInit() { return true; }
        public virtual bool SecondInit() { return true; }
        public virtual void FirstComplete() { }
        public virtual void SecondComplete() { }

        protected virtual void StartDialogProcessing(DialogQueue dlg) { }
        
        protected virtual void EndDialogProcessing(DialogQueue dlg) { }

        protected virtual void EndDialogEvent()
        {
            if (Lists.IsNotEmpty(nextStories))
            {
                foreach (var story in nextStories)
                {
                    var info = QuestFactory.Instance.GetStoryInfo(story.StoryID);
                    story.IsActive = true;
                    info.IsActive = true;
                }
            }

            if(hidePlayer)
                PlayerCharacter.SetActive(true);
            
            if(hideTopPanel)
                TopPanel.SetActive(true);
            
            Complete();
        }

        public virtual void Destruct()
        {
            if (destroyGameObject)
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

            if (useShowFade)
            {
                mainDialog.Run(() =>
                {
                    // Не добавляем RuntimeObjectList, чтобы скрипт доиграл до конца гарантированно
                    // Do not add RuntimeObjectList, so that the script is guaranteed to finish
                    StoryActionHelper.Fade(ObjectFinder.SceneViewImage, Color.white, Color.clear, 0.8f);
                });
            }

            var endDialog = new DialogQueue();
            EndDialogProcessing(endDialog);
            
            dialogBox.SetDialogQueueAndRun(mainDialog.Queue, endDialog.Queue, 0, this);
            
            storyInfo.Count++;
            QuestFactory.Instance.UpdateStoryInfo();
        }
        
    }
    
}