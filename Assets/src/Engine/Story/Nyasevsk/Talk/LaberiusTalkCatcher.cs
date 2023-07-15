using Engine.Logic.Dialog;

namespace Engine.Story.Nyasevsk.Talk
{
    
    public class LaberiusTalkCatcher : TalkSelectCatcherBase
    {
        public override string StoryID => "talk.nyasevsk1.laberius";

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Text("Тест");
        }
    }
    
}