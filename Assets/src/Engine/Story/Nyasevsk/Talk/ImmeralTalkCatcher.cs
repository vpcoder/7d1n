using Engine.Logic.Dialog;

namespace Engine.Story.Nyasevsk.Talk
{
    
    public class ImmeralTalkCatcher : TalkSelectCatcherBase
    {
        public override string StoryID => "talk.nyasevsk1.immeral";

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Text("Тест");
        }
    }
    
}