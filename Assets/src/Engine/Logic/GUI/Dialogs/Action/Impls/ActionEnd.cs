namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionEnd : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;

        public override void DoRun(DialogRuntime runtime)
        {
            runtime.End();
        }
    }
}