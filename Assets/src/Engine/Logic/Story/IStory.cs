using Engine.Logic.Dialog;

namespace Engine.Story

{
    public interface IStory
    {

        void Init();

        void CreateDialog(DialogQueue dlg);

    }
    
}