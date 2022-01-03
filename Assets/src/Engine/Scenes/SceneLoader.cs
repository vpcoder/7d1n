using Engine.Data;
using Engine.Data.Stories;
using Engine.IO;
using Engine.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class SceneLoader : MonoBehaviour
    {

        [SerializeField] private GameObject topPanelPrefab;
        [SerializeField] private GameObject scanUIPrefab;

        private bool exitFlag = false;

        private void Start()
        {
            ObjectFinder.Clear();
        }

        private void Awake()
        {
            exitFlag = true;
        }

        public void Load()
        {
            Debug.Log("Loaded!");
        }

        private void Update()
        {
            if (!exitFlag)
                return;

            var game = Game.Instance;
            var runtime = game.Runtime;

            SetupCanvasSettings();

            if(runtime.Scene == Scenes.SceneName.Location)
                ObjectFinder.Find<LocationLoader>().LoadLocation(runtime.Location);

            if(runtime.Scene != Scenes.SceneName.Menu) // Это не меню, выполняем полное сохранение
            {
                CharacterStory.Instance.SaveAll(Game.Instance.Character);
            }

            if (runtime.Scene == Scenes.SceneName.Map || runtime.Scene == Scenes.SceneName.Location)
            {
                LoadPanels();
            }

            if (runtime.Scene == Scenes.SceneName.Map)
            {
                LoadMapScene();
            }

            if (runtime.Mode == Mode.Switch)
                runtime.Mode = Mode.Game;

            Load();

            GameObject.Destroy(this);
            exitFlag = false;
        }

        private void SetupCanvasSettings()
        {
            var canvas = ObjectFinder.Get<Canvas>("Canvas");
            var canvasScaler = canvas.GetComponent<CanvasScaler>();

            canvasScaler.referenceResolution = new Vector2(1280, 1024);
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.referencePixelsPerUnit = 100;
        }

        private void LoadPanels()
        {
            var canvas = ObjectFinder.Get<Canvas>("Canvas");
            var panel = GameObject.Instantiate<GameObject>(topPanelPrefab, canvas.transform);
            panel.transform.name = "TopPanel";
            panel.transform.SetAsFirstSibling();
        }

        private void LoadMapScene()
        {
            var canvas = ObjectFinder.Get<Canvas>("Canvas").transform;

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
        }

    }

}
