using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Dialog.Action;
using JetBrains.Annotations;
using UnityEngine;

namespace Engine.Logic.Dialog
{
    public class DialogRuntime
    {
        private List<IActionCommand> dialogQueue;
        private List<IActionCommand> endDialogQueue;
        private int index;
        private int goToActionIndex;
        private bool isLastGoTo;

        public event System.Action EndEvent;
        public event System.Action StartEvent;
        
        public DialogBox DialogBox { get; private set; }

        public IActionCommand CurrentAction => GetByIndex(index);

        public bool IsWaitAnswer => IsWaitType(WaitType.WaitSelect);
        
        public bool IsWaitTime => IsWaitType(WaitType.WaitTime);

        public bool IsEnd => Lists.IsEmpty(dialogQueue) || index >= dialogQueue.Count;

        private bool IsWaitType(WaitType type)
        {
            if (Lists.IsEmpty(dialogQueue))
                return false;
                
            if (index < 0 || index >= dialogQueue.Count)
                return false;
                
            var action = dialogQueue[index];
            return action != null && action.WaitType == type;
        }
        
        private IActionCommand GetByIndex(int index)
        {
            if (Lists.IsEmpty(dialogQueue))
                return null;

            if (index < 0 || index >= dialogQueue.Count)
                return null;

            var action = dialogQueue[index];
            if (action != null && action.ConstructType == ConstructType.OnStartAction && !action.IsConstructed)
                action.Construct();

            return action;
        }
        
        public void SetDialogQueueAndRun([NotNull] DialogBox dialogBox,
                                         [NotNull] IEnumerable<IActionCommand> dialogQueue,
                                         IEnumerable<IActionCommand> endDialogQueue,
                                         int startIndex = 0)
        {
            if (StartEvent != null)
            {
                StartEvent.Invoke();
                foreach (var link in StartEvent.GetInvocationList())
                    StartEvent -= (System.Action)link;
            }

#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("setup dialog queue in runtime...");
#endif

            DialogBox = dialogBox;

            this.endDialogQueue = endDialogQueue == null ? null : endDialogQueue.ToList();
            if (Lists.IsNotEmpty(this.dialogQueue))
                End();

            this.dialogQueue = dialogQueue.ToList();

#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("current queue '" + this.dialogQueue.Count + "'...");
#endif

            index = startIndex;
            foreach (var action in this.dialogQueue)
                if (action != null && action.ConstructType == ConstructType.OnStartQueue && !action.IsConstructed)
                    action.Construct();
        }

        public void TryGoTo([NotNull] string actionID, bool isLastGoTo = false)
        {
            goToActionIndex = index;
            if (string.IsNullOrEmpty(actionID))
                return;
            
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("dialog goto action to '" + actionID + "'...");
#endif

            for (int i = 0; i < dialogQueue.Count; i++)
            {
                var action = dialogQueue[i];
                if (action?.ID == actionID)
                {
                    index = i;
                    var actionGoTo = GetByIndex(goToActionIndex);
                    if (actionGoTo != null && actionGoTo.IsConstructed && actionGoTo.DestructType == DestructType.OnEndAction)
                        actionGoTo.Destruct();
                    
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
                    Debug.Log("dialog goto action founded '" + action.GetType().Name + "'");
#endif
                    this.isLastGoTo = isLastGoTo;
                    return;
                }
            }

#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.LogError("dialog goto action not founded!");
#endif

            End();
        }

        public void Run()
        {
            for (;;)
            {
                var action = CurrentAction;
                if (action == null)
                {
                    End();
                    return;
                }

                action.Run(this);

                switch (action.WaitType)
                {
                    case WaitType.WaitClick:
                    case WaitType.WaitSelect:
                    case WaitType.WaitTime:
                        break;
                    case WaitType.NoWait:
                        Next();
                        continue;
                }

                break;
            }
        }

        public void Next()
        {
            if (isLastGoTo)
            {
                isLastGoTo = false;
                return;
            }

#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("next dialog action...");
#endif

            var action = CurrentAction;
            if (action != null && action.IsConstructed && action.DestructType == DestructType.OnEndAction)
                action.Destruct();

            if (action != null && Lists.IsNotEmpty(dialogQueue) && Lists.IsNotEmpty(action.DelayedDestruct))
                foreach (var item in dialogQueue)
                    if (action.DelayedDestruct.Contains(item.ID))
                        item.Destruct();

            if (Lists.IsEmpty(dialogQueue) || index < 0 || index >= dialogQueue.Count)
                End();
            
            index++;
        }

        public void End()
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("end dialog");
#endif

            if (Lists.IsNotEmpty(dialogQueue))
            {
                foreach (var action in dialogQueue)
                    if (action != null && action.IsConstructed)
                        action.Destruct();
                dialogQueue.Clear();
            }

            if (Lists.IsNotEmpty(endDialogQueue))
            {
                foreach (var action in endDialogQueue)
                {
                    if (action == null)
                        continue;
                    
                    if(!action.IsConstructed) {
                        action.Construct();
                    }
                    action.Run(this);
                    action.Destruct();
                }
                endDialogQueue.Clear();
            }

            dialogQueue = null;
            endDialogQueue = null;
            index = 0;

            if (DialogBox != null)
                DialogBox.Hide();
            
            if (EndEvent != null)
            {
                EndEvent.Invoke();
                foreach (var link in EndEvent.GetInvocationList())
                    EndEvent -= (System.Action)link;
            }
        }
    }
}