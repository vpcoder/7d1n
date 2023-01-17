using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Dialog.Action
{
    public abstract class ActionCommand : IActionCommand
    {
        public string ID { get; set; }

        public bool IsConstructed { get; private set; }

        public virtual ConstructType ConstructType => ConstructType.OnStartAction;

        public virtual DestructType DestructType => DestructType.OnEndAction;

        public virtual WaitType WaitType => WaitType.WaitClick;

        public virtual float WaitTime { get; set; } = 0f;
        public virtual float StartTime { get; private set; }

        public ISet<string> DelayedDestruct { get; } = new HashSet<string>();

        public ActionCommand()
        {
            ID = Guid.NewGuid().ToString();
        }
        
        public void Construct()
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("construct for '" + GetType().Name + "'...");
#endif

            DoConstruct();
            IsConstructed = true;
        }

        public void Destruct()
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("destruct for '" + GetType().Name + "'...");
#endif

            DoDestruct();
            IsConstructed = false;
        }

        public void Run(DialogRuntime runtime)
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("run for '" + GetType().Name + "'...");
#endif
            if (!IsConstructed)
                DoConstruct();

            StartTime = Time.time;
            DoRun(runtime);
        }

        public abstract void DoRun(DialogRuntime runtime);

        public virtual void DoConstruct()
        { }

        public virtual void DoDestruct()
        { }

        #region Tools

        public ActionCommand SetID(string id)
        {
            ID = id;
            return this;
        }

        #endregion
    }
}