
namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionGlobalText : ActionCommand
    {

        public override WaitType WaitType => WaitType.NoWait;
        
        public string Text { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            var dialogBox = runtime.DialogBox;
            dialogBox.SetGlobalText(runtime.ProcessText(Text));
        }
        
    }
}