using System.Collections.Generic;
using Engine.Data.Factories;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
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
        [SerializeField] private bool destroyStoreAfterComplete = true;
        [SerializeField] private bool rewriteSaveState = true;

        [SerializeField] private List<NextStory> nextStories;
        [SerializeField] private RunStoryContext context;

        public abstract string StoryID { get; }

        public virtual List<NextStory> NextStories => nextStories;
        
        public bool RewriteSaveState
        {
            get { return rewriteSaveState; }
            set { rewriteSaveState = value; }
        }
        
        public virtual bool IsDestroyStoreAfterComplete
        {
            get { return destroyStoreAfterComplete; }
            set { destroyStoreAfterComplete = value; }
        }
        
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
                if (context.TopPanel == null)
                    context.TopPanel = ObjectFinder.TopPanel;
                return context.TopPanel;
            }
        }

        public GameObject PlayerCharacter
        {
            get
            {
                if (context.PlayerCharacter == null)
                    context.PlayerCharacter = ObjectFinder.Character.MeshSwitcher.gameObject;
                return context.PlayerCharacter;
            }
        }

        public Vector3 PlayerEyePos => context.PlayerEyePos;

        private void Start()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("start story '" + StoryID + "'");
#endif

            if (IsDestroyStoreAfterComplete)
            {
                context.StoryInfo = QuestFactory.Instance.GetStoryInfo(this);
                IsActive = context.StoryInfo.IsActive;
            }

            LoadFactory.Instance.Complete += Init;
        }

        public virtual void Init()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("init story '" + StoryID + "'");
#endif

            bool canRun;

            if (IsDestroyStoreAfterComplete)
            {
                if (context.StoryInfo.IsComplete)
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

            if (IsDestroyStoreAfterComplete)
            {
                if (context.StoryInfo.IsComplete)
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
                context.StoryInfo.IsComplete = true;
                IsActive = false;
                QuestFactory.Instance.UpdateStoryInfo();
            }
            else
            {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
                Debug.Log("first complete story '" + StoryID + "'");
#endif
                FirstComplete();
            }

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
            
            if(IsDestroyStoreAfterComplete)
                Destruct();
        }
        
        public abstract void CreateDialog(DialogQueue dlg);

        public virtual bool FirstInit() { return true; }
        public virtual bool SecondInit() { return false; }
        public virtual void FirstComplete() { }
        public virtual void SecondComplete() { NextStories.Clear(); }

        protected virtual void StartDialogProcessing(DialogQueue dlg)
        {
            SaveState();
            if (RewriteSaveState)
            {
                dlg.Run(() =>
                {
                    SetupDialogState();
                });
            }
        }

        protected virtual void EndDialogProcessing(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                ResetState();
            });
        }

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
            var playerCharacter = ObjectFinder.Character;
            context.PlayerEyePos = playerCharacter.Eye.position;
            playerCharacter.StopMove();
            
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
            
            context.StoryInfo.Count++;
            QuestFactory.Instance.UpdateStoryInfo();
        }

        public virtual void SaveState()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("save camera state for story '" + StoryID + "'");
#endif
            var cameraLink = Camera.main;
            context.StartFov = cameraLink.fieldOfView;
            context.StartTransformPair = cameraLink.GetState();
            context.StartFloor = ObjectFinder.Find<GlobalFloorSwitchController>().CurrentFloor;
        }

        public virtual void SetupDialogState()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("setup camera state for story '" + StoryID + "'");
#endif
            var cameraLink = Camera.main;
            cameraLink.fieldOfView = 60f;
            ObjectFinder.Find<GlobalFloorSwitchController>().SetMaxFloor();
        }

        public void ResetState()
        {
#if UNITY_EDITOR && DEBUG && STORY_DEBUG
            Debug.Log("restore camera state for story '" + StoryID + "'");
#endif
            var cameraLink = Camera.main;
            cameraLink.fieldOfView = context.StartFov;
            cameraLink.transform.SetState(context.StartTransformPair);
            ObjectFinder.Find<GlobalFloorSwitchController>().SetFloor(context.StartFloor);
        }

    }
    
}