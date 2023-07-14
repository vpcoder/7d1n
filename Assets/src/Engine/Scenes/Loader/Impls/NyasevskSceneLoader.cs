using Engine.Data;
using Engine.Data.Repositories;
using Engine.Scenes.Loader;

namespace src.Engine.Scenes.Loader.Impls
{
    
    public class NyasevskSceneLoader : SceneLoaderBase
    {
        
        protected override void OnLoad(LoadContext context)
        {
            // TODO: Убрать
            CharacterRepository.Instance.LoadAll(Game.Instance.Character);
        }

    }
    
}
