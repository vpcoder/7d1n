using Engine.Logic.Dialog;

namespace Engine.Story

{
    public interface IStory : IStoryActive
    {

        void Init();

        void CreateDialog(DialogQueue dlg);

    }
    
}