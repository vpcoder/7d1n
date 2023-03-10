using Engine.EGUI;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
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
            UIHintMessageManager.Show(hintMessagePrefab, PlayerEyePos, "Слишком далеко. Нужно подойти ближе.");
        }

        protected override void StartDialogProcessing(DialogQueue dlg)
        {
            base.StartDialogProcessing(dlg);
            
            dlg.Run(() =>
            {
                var camera = Camera.main;
                startFov = camera.fieldOfView;
                startTransformPair = camera.GetState();
                camera.fieldOfView = 60f;

                var floorController = ObjectFinder.Find<FloorSwitchController>();
                startFloor = floorController.CurrentFloor;
                floorController.SetMaxFloor();
            });
        }

        protected override void EndDialogProcessing(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.fieldOfView = startFov;
                Camera.main.transform.SetState(startTransformPair);
                ObjectFinder.Find<FloorSwitchController>().SetFloor(startFloor);
            });
            base.EndDialogProcessing(dlg);
        }

        private int startFloor;
        private float startFov;
        private TransformPair startTransformPair;

    }
    
}