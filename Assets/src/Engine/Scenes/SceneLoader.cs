using Engine.Data;
using Engine.Scenes.Loader;
using src.Engine.Scenes.Loader;
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
            
            var loadContext = new LoadContext()
            {
                TopPanel = topPanelPrefab,
                ScanUI = scanUIPrefab,
                LocationGUI = locationGUIPrefab,
            };
            
            LoadFactory.Instance.PreLoad(runtime.Scene, loadContext);

            SetupCanvasSettings();

            LoadFactory.Instance.Load(runtime.Scene, loadContext);
            
            if (runtime.Mode == Mode.Switch)
                runtime.Mode = Mode.Game;

            Destroy(this);
        }
        
        private static void SetupCanvasSettings()
        {
            Debug.Log("setup canvas...");

            var canvas = ObjectFinder.Get<Canvas>("Canvas");
            var canvasScaler = canvas.GetComponent<CanvasScaler>();

            canvasScaler.referenceResolution = new Vector2(1280, 1024);
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.referencePixelsPerUnit = 100;
        }

    }

}
