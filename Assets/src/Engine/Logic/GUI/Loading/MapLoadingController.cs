using Engine.EGUI;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class MapLoadingController : Panel
    {

        /// <summary>
        /// Значок загрузки тайлов
        /// </summary>
        [SerializeField] private Image inetImage;

        /// <summary>
        /// Анимация мигания значка загрузки тайлов
        /// </summary>
        [SerializeField] private AnimationCurve curve;

        /// <summary>
        /// Экземпляр карты mapbox
        /// </summary>
        [SerializeField] private AbstractMap map;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private AbstractLocationProvider locationProvider;

        private void Awake()
        {
            map.OnInitialized += map_OnInitialized;
            map.OnEditorPreviewEnabled += OnEditorPreviewEnabled;
            map.OnEditorPreviewDisabled += OnEditorPreviewDisabled;
        }

        private void map_OnInitialized()
        {
            var visualizer = map.MapVisualizer;
            visualizer.OnMapVisualizerStateChanged += (state) =>
            {
                if (this == null)
                    return;

                switch(state)
                {
                    case ModuleState.Finished:
                        inetImage.gameObject.SetActive(false);
                        break;
                    case ModuleState.Working:
                        inetImage.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            };
        }

        private void OnEditorPreviewEnabled()
        {
            inetImage.gameObject.SetActive(false);
        }

        private void OnEditorPreviewDisabled()
        {
            inetImage.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!inetImage.gameObject.activeInHierarchy)
                return;

            var progress = curve.Evaluate(Time.time);
            inetImage.color = Color.Lerp(Color.clear, Color.white, progress);
        }

    }

}
