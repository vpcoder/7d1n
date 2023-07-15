using Engine.Logic.Dialog;

namespace Engine.Story.Nyasevsk.Talk
{
    
    public class SecurityTalkCatcher : TalkSelectCatcherBase
    {
        public override string StoryID => "talk.nyasevsk1.security";

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Text("Тест");
        }
    }
    
}