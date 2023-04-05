
using UnityEngine;

namespace Engine.Logic.Dialog.Action.Impls
{
    
    public class ActionSound : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public string Sound { get; set; }
        public AudioSource Source { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            if (Source != null)
            {
                AudioController.Instance.PlaySound(Source, Sound);
                return;
            }
            AudioController.Instance.PlaySound(Sound);
        }
        
    }
    
}