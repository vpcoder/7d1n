using Engine.Data;
using Engine.Data.Stories;
using Engine.Logic;
using Engine.Scenes;
using Engine.Scenes.Loader;
using UnityEngine;

namespace src.Engine.Scenes.Loader.Impls
{
    
    public class LocationSceneLoader : SceneLoaderBase
    {
        
        public override SceneName Scene => SceneName.Location;
        
        protected override void OnLoad(LoadContext context)
        {
            Debug.Log("save character data...");
            CharacterStory.Instance.SaveAll(Game.Instance.Character);
            
            Debug.Log("load location ui...");

            var canvas = ObjectFinder.Get<Canvas>("Canvas");

            var panel = Object.Instantiate(context.TopPanel, canvas.transform);
            panel.transform.name = "TopPanel";
            panel.transform.SetAsFirstSibling();

            var gui = Object.Instantiate(context.LocationGUI, canvas.transform);
            gui.transform.name = "GUI";
            gui.transform.SetAsFirstSibling();

            ObjectFinder.Get("SceneView").SetAsFirstSibling();
        }

        protected override void OnPostLoad(LoadContext context)
        {
            // Выполняем загрузку локации
            ObjectFinder.Find<LocationLoader>().LoadLocation(Game.Instance.Runtime.Location);
        }
    }
    
}
