using System.Collections.Generic;

namespace Engine.Logic.Dialog.Action
{
    public enum ConstructType
    {
        OnStartQueue,
        OnStartAction
    }

    public enum DestructType
    {
        OnEndAction,
        OnDelayed,
        OnEndQueue,
    }

    public enum WaitType
    {
        NoWait,
        WaitClick,
        WaitSelect,
        WaitTime,
    }

    public interface IActionCommand
    {
        string ID { get; set; }

        bool IsConstructed { get; }
        ConstructType ConstructType { get; }
        DestructType DestructType { get; }
        WaitType WaitType { get; }
        ISet<string> DelayedDestruct { get; }
        float WaitTime { get; }
        float StartTime { get; }

        void Construct();
        void Run(DialogRuntime runtime);
        void Destruct();
    }
}