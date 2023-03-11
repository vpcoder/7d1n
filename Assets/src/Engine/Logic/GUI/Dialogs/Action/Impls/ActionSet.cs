
using System;

namespace Engine.Logic.Dialog.Action.Impls
{
    
    public class ActionSet : ActionCommand
    {
        
        public override WaitType WaitType => WaitType.NoWait;
        public string Name { get; set; }
        public string Value { get; set; }
        public Func<string> Callback { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            runtime.SetVariable(Name, Callback == null ? Value : Callback.Invoke());
        }
        
    }
    
}