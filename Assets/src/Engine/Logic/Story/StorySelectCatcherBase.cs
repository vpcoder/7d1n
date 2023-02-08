using Engine.EGUI;
using UnityEngine;

namespace Engine.Story
{
    
    public abstract class StorySelectCatcherBase : StoryBase, IStorySelectCatcher
    {

        [SerializeField] private bool destroyOnFirstSelect = false;
        
        [SerializeField] private GameObject hintQuestPrefab;
        [SerializeField] private GameObject hintMessagePrefab;
        
        private UIHintMessage hintLink;
        
        private void Start()
        {
            if(hintQuestPrefab != null)
                hintLink = UIHintMessageManager.ShowQuestHint(hintQuestPrefab, transform.position);
        }

        protected void Destruct()
        {
            if (hintLink != null)
                Destroy(hintLink.gameObject);
            
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<StoryObjectSelector>());
            Destroy(this);
        }

        public virtual void SelectInDistance()
        {
            RunDialog();
            
            if(destroyOnFirstSelect)
                Destruct();
        }
        
        public virtual void SelectOutDistance()
        {
            UIHintMessageManager.Show(hintMessagePrefab, PlayerCharacter.transform.position, "Слишком далеко. Нужно подойти ближе.");
        }

    }
    
}