using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Dialog.Action;
using Engine.Logic.Dialog.Action.Impls;

namespace Engine.Logic.Dialog
{
    
    public class DialogQueue
    {
        
        public List<IActionCommand> Queue { get; } = new List<IActionCommand>();
        
        public ActionAddPrefab Prefab(string prefabID)
        {
            var action = new ActionAddPrefab()
            {
                PrefabID = prefabID,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionText Text(string text, string first = null, string second = null)
        {
            var action = new ActionText
            {
                Text = text,
                FirstAvatar = first,
                SecondAvatar = second,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionSelect Select(List<SelectVariant> variants, string text = null, string first = null, string second = null)
        {
            var action = new ActionSelect
            {
                Variants = variants,
                Text = text,
                FirstAvatar = first,
                SecondAvatar = second,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionEnd End()
        {
            var action = new ActionEnd();
            Queue.Add(action);
            return action;
        }
        
        public ActionEmpty Empty()
        {
            var action = new ActionEmpty();
            Queue.Add(action);
            return action;
        }

        public ActionMusic Music(string music)
        {
            var action = new ActionMusic()
            {
                Music = music,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionDelay Delay(float waitTime, string showText = null)
        {
            var action = new ActionDelay()
            {
                WaitTime = waitTime,
                HiddenMode = false,
                ShowText = showText,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionDelay Delay(float waitTime, bool hiddenMode = false)
        {
            var action = new ActionDelay()
            {
                WaitTime = waitTime,
                HiddenMode = hiddenMode,
                ShowText = null,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionSound Sound(string sound)
        {
            var action = new ActionSound()
            {
                Sound = sound,
            };
            Queue.Add(action);
            return action;
        }

        public ActionRun Run(System.Action executable)
        {
            var action = new ActionRun()
            {
                Executable = executable,
            };
            Queue.Add(action);
            return action;
        }
        
        public ActionIf IfGoTo(Func<bool> condition, string trueGoTo, string falseGoTo = null)
        {
            var action = new ActionIf()
            {
                Condition = condition,
                TrueGoTo = trueGoTo,
                FalseGoTo = falseGoTo,
            };
            Queue.Add(action);
            return action;
        }
        
    }
    
}