using UnityEngine;

namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionIf : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public System.Func<bool> Condition { get; set; }
        public string TrueGoTo { get; set; }
        public string FalseGoTo { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            if (Condition == null)
            {
                Debug.LogError("nullpointer condition!");
                return;
            }
            
            runtime.TryGoTo(Condition.Invoke() ? TrueGoTo : FalseGoTo, true);
        }
    }
}