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
        private int index;
        private int goToActionIndex;
        private bool isLastGoTo;

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
        
        public void SetDialogQueueAndRun(DialogBox dialogBox, [NotNull] IEnumerable<IActionCommand> dialogQueue,
            int startIndex = 0)
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("setup dialog queue in runtime...");
#endif

            DialogBox = dialogBox;
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

            for (; index < dialogQueue.Count; index++)
            {
                var action = dialogQueue[index];
                if (action?.ID == actionID)
                {
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

            dialogQueue = null;
            index = 0;

            if (DialogBox != null)
                DialogBox.Hide();
        }
    }
}