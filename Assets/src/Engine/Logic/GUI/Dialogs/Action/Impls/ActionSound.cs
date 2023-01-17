
namespace Engine.Logic.Dialog.Action.Impls
{
    
    public class ActionSound : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public string Sound { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            AudioController.Instance.PlaySound(Sound);
        }
        
    }
    
}