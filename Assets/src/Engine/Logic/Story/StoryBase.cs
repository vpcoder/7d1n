using System;
using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Dialog;
using src.Engine.Scenes.Loader;
using UnityEngine;

namespace Engine.Story
{

    [Serializable]
    public class NextStory
    {
        [SerializeField] public StoryBase Story;
        [SerializeField] public bool SwitchActive;
        [SerializeField] public bool NeedRun;
    }
    
    public abstract class StoryBase : MonoBehaviour, IStory
    {

        [SerializeField] protected bool activeFlag = true;
        [SerializeField] protected bool needRunOnStartFlag = false;
        
        [SerializeField] private bool useShowFade = true;
        [SerializeField] private bool hidePlayer;
        [SerializeField] private bool hideTopPanel = true;
        [SerializeField] private bool destroyGameObject = true;

        [SerializeField] private List<NextStory> nextStories;
        
        private Vector3 playerEyePos;
        private GameObject topPanel;
        private GameObject playerCharacter;
        private StoryDataRepoObject storyInfo;

        public abstract string StoryID { get; }

        public virtual List<NextStory> NextStories => nextStories;
        
        public virtual bool IsActive
        {
            get { return activeFlag; }
            set
            {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                if(activeFlag != value)
                    Debug.Log("story '" + StoryID + "' active = " + value);
#endif
                activeFlag = value;
            }
        }

        public virtual bool IsNeedRunOnStart
        {
            get { return needRunOnStartFlag; }
            set { needRunOnStartFlag = value; }
        }
        
        public virtual bool IsUseShowFade
        {
            get { return useShowFade; }
            set { useShowFade = value; }
        }

        public virtual bool IsHidePlayer
        {
            get { return hidePlayer; }
            set { hidePlayer = value; }
        }

        public virtual bool IsHideTopPanel
        {
            get { return hideTopPanel; }
            set { hideTopPanel = value; }
        }
        
        public virtual bool IsDestroyGameObject
        {
            get { return destroyGameObject; }
            set { destroyGameObject = value; }
        }

        public void SetActiveAndSave()
        {
            IsActive = true;
            QuestFactory.Instance.UpdateStoryInfo();
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
                    playerCharacter = ObjectFinder.Character.MeshSwitcher.gameObject;
                return playerCharacter;
            }
        }

        public Vector3 PlayerEyePos => playerEyePos;

        private void Start()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("start story '" + StoryID + "'");
#endif
            
            storyInfo = QuestFactory.Instance.GetStoryInfo(this);
            
            LoadFactory.Instance.Complete += Init;
            IsActive = storyInfo.IsActive;
        }

        public virtual void Init()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("init story '" + StoryID + "'");
#endif

            bool canRun;
            if (storyInfo.IsComplete)
            {
                canRun = SecondInit();
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                Debug.Log("second init story '" + StoryID + "'");
#endif
            }
            else
            {
                canRun = FirstInit();
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                Debug.Log("first init story '" + StoryID + "'");
#endif
            }
            
            if(canRun && IsNeedRunOnStart)
                RunDialog();
            
            if(!canRun)
                Complete();
        }

        public virtual void Complete()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("complete story '" + StoryID + "'");
#endif
            
            var dialogBox = ObjectFinder.DialogBox;
            dialogBox.Runtime.StartEvent -= StartDialogEvent;
            dialogBox.Runtime.EndEvent   -= EndDialogEvent;

            if (storyInfo.IsComplete)
            {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                Debug.Log("second complete story '" + StoryID + "'");
#endif
                SecondComplete();
            }
            else
            {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                Debug.Log("first complete story '" + StoryID + "'");
#endif
                FirstComplete();
            }

            storyInfo.IsComplete = true;
            IsActive = false;
            QuestFactory.Instance.UpdateStoryInfo();
            
            if (Lists.IsNotEmpty(NextStories))
            {
                foreach (var storyData in NextStories)
                {
                    var story = storyData.Story;
                    var info = QuestFactory.Instance.GetStoryInfo(story.StoryID);

                    if (storyData.SwitchActive)
                    {
                        story.IsActive = true;
                        info.IsActive = true;
                    }

                    if (storyData.NeedRun)
                    {
                        story.RunDialog();
                    }
                }
            }
            
            Destruct();
        }
        
        public abstract void CreateDialog(DialogQueue dlg);

        public virtual bool FirstInit() { return true; }
        public virtual bool SecondInit() { return false; }
        public virtual void FirstComplete() { }
        public virtual void SecondComplete() { NextStories.Clear(); }

        protected virtual void StartDialogProcessing(DialogQueue dlg) { }
        
        protected virtual void EndDialogProcessing(DialogQueue dlg) { }

        protected virtual void EndDialogEvent()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("show ui for '" + StoryID + "'");
#endif
            if(hidePlayer)
                PlayerCharacter.SetActive(true);
            
            if(hideTopPanel)
                TopPanel.SetActive(true);
            
            Complete();
        }

        public virtual void Destruct()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("destruct story '" + StoryID + "'");
#endif
            if (destroyGameObject)
                Destroy(gameObject);
        }
        
        protected virtual void StartDialogEvent()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("hide ui for '" + StoryID + "'");
#endif
            if (hidePlayer)
                PlayerCharacter.SetActive(false);
            
            if(hideTopPanel)
                TopPanel.SetActive(false);
        }
        
        public void RunDialog()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("run story '" + StoryID + "'");
#endif
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
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                    Debug.Log("fade off story '" + StoryID + "'");
#endif
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