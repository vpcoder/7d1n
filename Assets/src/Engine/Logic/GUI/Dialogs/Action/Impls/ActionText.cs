﻿using Engine.Data.Factories;

namespace Engine.Logic.Dialog.Action.Impls
{
    public class ActionText : ActionCommand
    {
        
        public string FirstAvatar { get; set; } = null;
        
        public string SecondAvatar { get; set; } = null;

        public string Text { get; set; }

        public override void DoRun(DialogRuntime runtime)
        {
            var dialogBox = runtime.DialogBox;
            dialogBox.SetText(Text);
            dialogBox.SetFirstAvatar(AvatarFactory.Instance.Get(FirstAvatar));
            dialogBox.SetSecondAvatar(AvatarFactory.Instance.Get(SecondAvatar));
        }
        
    }
}