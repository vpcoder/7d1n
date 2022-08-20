
namespace Engine.Logic.Load
{

    public interface ISceneLoadProcessor
    {

        bool IsLoaded { get; }

        void StartLoad();

        void CompleteLoad();

        void SetTitle(string title);

        void SetDescription(string message);

    }

}
