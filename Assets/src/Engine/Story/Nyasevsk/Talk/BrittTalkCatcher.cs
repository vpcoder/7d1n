using Engine.Logic.Dialog;

namespace Engine.Story.Nyasevsk.Talk
{
    
    public class BrittTalkCatcher : TalkSelectCatcherBase
    {
        public override string StoryID => "talk.nyasevsk1.britt";

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Text("Тест");
        }
    }
    
}