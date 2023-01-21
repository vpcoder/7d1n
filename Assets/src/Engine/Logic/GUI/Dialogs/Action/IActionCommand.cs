using System.Collections.Generic;

namespace Engine.Logic.Dialog.Action
{

    public interface IActionCommand
    {

        #region Properties

        string ID { get; set; }
        
        bool IsConstructed { get; }
        ConstructType ConstructType { get; }
        DestructType DestructType { get; }
        WaitType WaitType { get; }
        ISet<string> DelayedDestruct { get; }
        float WaitTime { get; }
        float StartTime { get; }
        
        #endregion

        #region Methods

        void Construct();
        void Run(DialogRuntime runtime);
        void Destruct();

        #endregion
        
    }
    
}