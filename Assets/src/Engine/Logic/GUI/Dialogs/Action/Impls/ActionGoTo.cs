
namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionGoTo : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public string GoTo { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            runtime.TryGoTo(GoTo, true);
        }
    }
}