using UnityEngine;

namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionDelay : ActionCommand
    {
        public override WaitType WaitType => WaitType.WaitTime;

        public bool HiddenMode { get; set; }
        public string ShowText { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            var dialogBox = runtime.DialogBox;
            
            if(ShowText != null)
                dialogBox.SetText(ShowText);
            
            if (HiddenMode)
                dialogBox.Body.SetActive(false);
        }

        public override void DoDestruct()
        {
            base.DoDestruct();
            
            if (HiddenMode)
                ObjectFinder.DialogBox.Body.SetActive(true);
        }
    }
}