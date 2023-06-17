using System;
using UnityEngine;

namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionRun : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public System.Action Executable { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            try
            {
                Executable?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }
}