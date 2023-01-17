using Engine.Data.Factories;
using UnityEngine;

namespace Engine.Logic.Dialog.Action.Impls
{
    
    public class ActionAddPrefab : ActionCommand
    {
        public override ConstructType ConstructType => ConstructType.OnStartAction;
        public override DestructType DestructType => DestructType.OnDelayed;
        public override WaitType WaitType => WaitType.NoWait;
        public string PrefabID { get; set; }
        
        private GameObject prefabInstance;

        public override void DoRun(DialogRuntime runtime)
        {
            prefabInstance.SetActive(true);
        }

        public override void DoConstruct()
        {
            prefabInstance = Object.Instantiate(PrefabFactory.Instance.Get(PrefabID), ObjectFinder.Canvas.transform);
            prefabInstance.SetActive(false);
        }

        public override void DoDestruct()
        {
            base.DoDestruct();
            prefabInstance.SetActive(false);
            Object.Destroy(prefabInstance);
        }
    }
    
}