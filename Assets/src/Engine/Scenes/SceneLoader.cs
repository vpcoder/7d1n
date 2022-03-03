using Engine.Data;
using Engine.Data.Stories;
using Engine.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class SceneLoader : MonoBehaviour
    {

        [SerializeField] private GameObject topPanelPrefab;
        [SerializeField] private GameObject scanUIPrefab;
        [SerializeField] private GameObject locationGUIPrefab;

        private void Start()
        {
            ObjectFinder.Clear();
        }

        private void Awake()
        {
            Debug.Log("switch loaded scene...");

            var game = Game.Instance;
            var runtime = game.Runtime;

            SetupCanvasSettings();

            if (runtime.Scene == Scenes.SceneName.Location)
                ObjectFinder.Find<LocationLoader>().LoadLocation(runtime.Location);

            if (runtime.Scene != Scenes.SceneName.Menu) // Это не меню, выполняем полное сохранение
            {
                CharacterStory.Instance.SaveAll(Game.Instance.Character);
            }

            switch (runtime.Scene)
            {
                case Scenes.SceneName.Map:
                    LoadMapScene();
                    break;
                case Scenes.SceneName.Location:
                    LoadLocationScene();
                    break;
            }

            if (runtime.Mode == Mode.Switch)
                runtime.Mode = Mode.Game;

            Load();

            GameObject.Destroy(this);
        }

        public void Load()
        {
            Debug.Log("Loaded!");
        }

        private void SetupCanvasSettings()
        {
            Debug.Log("setup canvas...");

            var canvas = ObjectFinder.Get<Canvas>("Canvas");
            var canvasScaler = canvas.GetComponent<CanvasScaler>();

            canvasScaler.referenceResolution = new Vector2(1280, 1024);
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.referencePixelsPerUnit = 100;
        }

        private void LoadLocationScene()
        {
            Debug.Log("load location ui...");

            var canvas = ObjectFinder.Get<Canvas>("Canvas");

            var panel = GameObject.Instantiate<GameObject>(topPanelPrefab, canvas.transform);
            panel.transform.name = "TopPanel";
            panel.transform.SetAsFirstSibling();

            var gui = GameObject.Instantiate<GameObject>(locationGUIPrefab, canvas.transform);
            gui.transform.name = "GUI";
            gui.transform.SetAsFirstSibling();

            ObjectFinder.Get("SceneView").SetAsFirstSibling();
        }

        private void LoadMapScene()
        {
            Debug.Log("load map ui...");

            var canvas = ObjectFinder.Get<Canvas>("Canvas").transform;

            var panel = GameObject.Instantiate<GameObject>(topPanelPrefab, canvas.transform);
            panel.transform.name = "TopPanel";
            panel.transform.SetAsFirstSibling();

            var game = Game.Instance;
            var runtime = game.Runtime;

            var mapPlayer = ObjectFinder.Find<MapHuman>();
            mapPlayer.transform.localPosition = runtime.PlayerPosition;
            mapPlayer.MoveContext = runtime.PlayerContext;

            var characterMap = ObjectFinder.Find<MapCharacter>();
            characterMap.transform.localPosition = runtime.CharacterPosition;
            characterMap.MoveContext = runtime.CharacterContext;

            var scanPanel = GameObject.Instantiate<GameObject>(scanUIPrefab, canvas);
            scanPanel.transform.SetAsFirstSibling();

            ObjectFinder.Get("SceneView").SetAsFirstSibling();
        }

    }

}
