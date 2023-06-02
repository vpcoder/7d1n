using Engine.Data.Factories;
using Engine.EGUI;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story
{
    
    public abstract class StorySelectCatcherBase : StoryBase, IStorySelectCatcher
    {

        [SerializeField] private bool destroyStoryObject = false;
        [SerializeField] private bool showQuestHint = true;

        private UIHintMessage hintLink;

        public bool RewriteSaveState
        {
            get;
            set;
        } = true;

        public override bool FirstInit()
        {
            if (!base.FirstInit())
                return false;
            
            if (showQuestHint && activeFlag && hintLink == null)
            {
                var questHintPrefab = EffectFactory.Instance.Get(EffectFactory.QUEST_HINT);
                hintLink = UIHintMessageManager.ShowQuestHint(questHintPrefab, transform.position);
            }

            return true;
        }
        
        public override bool IsActive
        {
            get { return base.IsActive; }
            set
            {
                base.IsActive = value;

                if (value && showQuestHint && hintLink == null)
                {
#if UNITY_EDITOR
                    if (Application.isPlaying)
                    {
#endif
                        var questHintPrefab = EffectFactory.Instance.Get(EffectFactory.QUEST_HINT);
                        hintLink = UIHintMessageManager.ShowQuestHint(questHintPrefab, transform.position);
#if UNITY_EDITOR
                    }
#endif
                }
            }
        }

        public override void Destruct()
        {
            if (hintLink != null)
                Destroy(hintLink.gameObject);

            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<StoryObjectSelector>());
            Destroy(this);

            base.Destruct();
        }

        public virtual void SelectInDistance()
        {
            RunDialog();
            
            if(destroyStoryObject)
                Destruct();
        }
        
        public virtual void SelectOutDistance()
        {
            var messageHintPrefab = EffectFactory.Instance.Get(EffectFactory.QUEST_HINT);
            UIHintMessageManager.Show(messageHintPrefab, PlayerEyePos, Localization.Instance.Get("msg_error_cant_use_story"));
        }

        protected override void StartDialogProcessing(DialogQueue dlg)
        {
            base.StartDialogProcessing(dlg);

            if (RewriteSaveState)
            {
                dlg.Run(() =>
                {
                    SaveState();
                    SetupDialogState();
                });
            }
        }

        public void SaveState()
        {
            var camera = Camera.main;
            startFov = camera.fieldOfView;
            startTransformPair = camera.GetState();
            startFloor = ObjectFinder.Find<FloorSwitchController>().CurrentFloor;
        }

        public void SetupDialogState()
        {
            var camera = Camera.main;
            camera.fieldOfView = 60f;
            ObjectFinder.Find<FloorSwitchController>().SetMaxFloor();
        }

        public void ResetState()
        {
            Camera.main.fieldOfView = startFov;
            Camera.main.transform.SetState(startTransformPair);
            ObjectFinder.Find<FloorSwitchController>().SetFloor(startFloor);
        }

        protected override void EndDialogProcessing(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                ResetState();
            });
            base.EndDialogProcessing(dlg);
        }

        private int startFloor;
        private float startFov;
        private TransformPair startTransformPair;

    }
    
}