using Engine.Data.Factories;
using Engine.EGUI;
using UnityEngine;

namespace Engine.Story
{
    
    /// <summary>
    ///
    /// Абстрактная история запускаемая по селектору (игрок взаимодействует с областью в сцене тычком пальца)
    /// ---
    /// Abstract story triggered by a selector (player interacts with an area in the scene with a tap of a finger)
    /// 
    /// </summary>
    public abstract class SelectCatcherBase : StoryBase, IStorySelectCatcher
    {

        [SerializeField] private bool showHint = true;

        private UIHintMessage hintLink;

        protected abstract string HintID { get; }

        public override bool FirstInit()
        {
            if (!base.FirstInit())
                return false;
            
            if (showHint && activeFlag && hintLink == null && HintID != null)
            {
                var questHintPrefab = EffectFactory.Instance.Get(HintID);
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
                UpdateHintState();
            }
        }

        public void UpdateHintState()
        {
            if (IsActive && showHint && hintLink == null && HintID != null)
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
#endif
                    var hintPrefab = EffectFactory.Instance.Get(HintID);
                    hintLink = UIHintMessageManager.ShowQuestHint(hintPrefab, transform.position);
#if UNITY_EDITOR
                }
#endif
            }
            else
            {
                DestructHint();
            }
        }

        protected void DestructHint()
        {
            if (hintLink != null)
                Destroy(hintLink.gameObject);
        }
        
        public override void Destruct()
        {
            DestructHint();

            var collider = GetComponent<Collider>();
            if(collider != null)
                Destroy(collider);

            var story = GetComponent<StoryObjectSelector>();
            if(story != null)
                Destroy(story);
            
            Destroy(this);

            base.Destruct();
        }

        public virtual void SelectInDistance()
        {
            RunDialog();
        }
        
        public virtual void SelectOutDistance()
        {
            var messageHintPrefab = EffectFactory.Instance.Get(EffectFactory.MESSAGE_HINT);
            UIHintMessageManager.Show(messageHintPrefab, PlayerEyePos, Localization.Instance.Get("msg_error_cant_use_story"));
        }

    }
    
}