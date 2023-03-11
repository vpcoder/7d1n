using System;
using System.Collections.Generic;
using Engine.Data.Factories;

namespace Engine.Logic.Dialog.Action.Impls
{
    
    public class SelectVariant
    {
        
        public string ID { get; private set; }
        public string Text { get; set; }
        public string GoTo { get; private set; }

        public static string Point => Guid.NewGuid().ToString();

        public static SelectVariant New(string text, string goTo)
        {
            return new SelectVariant()
            {
                Text = text,
                GoTo = goTo,
                ID = Point,
            };
        }

    }

    public class ActionSelect : ActionCommand
    {
        
        public override WaitType WaitType => WaitType.WaitSelect;
        public List<SelectVariant> Variants { get; set; }
        public string Text { get; set; }
        public string FirstAvatar { get; set; } = null;
        
        public string SecondAvatar { get; set; } = null;

        public override void DoRun(DialogRuntime runtime)
        {
            var dialogBox = runtime.DialogBox;
            
            foreach (var variant in Variants)
                variant.Text = runtime.ProcessText(variant.Text);

            dialogBox.SetVariants(Variants);
            dialogBox.SetText(runtime.ProcessText(Text));
            dialogBox.SetFirstAvatar(AvatarFactory.Instance.Get(FirstAvatar));
            dialogBox.SetSecondAvatar(AvatarFactory.Instance.Get(SecondAvatar));
        }
        
    }
    
}