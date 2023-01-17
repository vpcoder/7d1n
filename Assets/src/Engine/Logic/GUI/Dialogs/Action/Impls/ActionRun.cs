namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionRun : ActionCommand
    {
        public override WaitType WaitType => WaitType.NoWait;
        public System.Action Executable { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            Executable?.Invoke();
        }
    }
}