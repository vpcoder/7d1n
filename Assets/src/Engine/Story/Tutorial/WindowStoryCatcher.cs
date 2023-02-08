using Engine.Logic.Dialog;

namespace Engine.Story.Tutorial
{
    
    public class WindowStoryCatcher : StorySelectCatcherBase
    {

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Text("test");
            dlg.End();
        }
        
    }
    
}
