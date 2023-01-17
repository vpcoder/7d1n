
namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionMusic : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public string Music { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            AudioController.Instance.PlayMusic(Music);
        }
        
    }
}