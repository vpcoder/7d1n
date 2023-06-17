using Engine.Data.Factories;
using Engine.EGUI;
using UnityEngine;

namespace Engine.Story
{
    
    public abstract class StorySelectCatcherBase : StoryBase, IStorySelectCatcher
    {

        [SerializeField] private bool showQuestHint = true;

        private UIHintMessage hintLink;
        
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
        }
        
        public virtual void SelectOutDistance()
        {
            var messageHintPrefab = EffectFactory.Instance.Get(EffectFactory.QUEST_HINT);
            UIHintMessageManager.Show(messageHintPrefab, PlayerEyePos, Localization.Instance.Get("msg_error_cant_use_story"));
        }

    }
    
}