using Engine.Data;
using Engine.Data.Stories;
using Engine.Logic.Locations;
using Engine.Map;
using Engine.Scenes.Loader;
using UnityEngine;

namespace src.Engine.Scenes.Loader.Impls
{
    
    public class MapSceneLoader : SceneLoaderBase
    {

        protected override void OnLoad(LoadContext context)
        {
            Debug.Log("save character data...");
            CharacterStory.Instance.SaveAll(Game.Instance.Character);
            
            Debug.Log("load map ui...");

            var canvas = ObjectFinder.Get<Canvas>("Canvas").transform;

            var panel = Object.Instantiate(context.TopPanel, canvas.transform);
            panel.transform.name = "TopPanel";
            panel.transform.SetAsFirstSibling();
            
            Object.Destroy(ObjectFinder.Find<BattleManager>().gameObject);

            var game = Game.Instance;
            var runtime = game.Runtime;

            var mapPlayer = ObjectFinder.Find<MapHuman>();
            mapPlayer.transform.localPosition = runtime.PlayerPosition;
            mapPlayer.MoveContext = runtime.PlayerContext;

            var characterMap = ObjectFinder.Find<MapCharacter>();
            characterMap.transform.localPosition = runtime.CharacterPosition;
            characterMap.MoveContext = runtime.CharacterContext;

            var scanPanel = Object.Instantiate(context.ScanUI, canvas);
            scanPanel.transform.SetAsFirstSibling();

            ObjectFinder.Get("SceneView").SetAsFirstSibling();
        }

    }
    
}
