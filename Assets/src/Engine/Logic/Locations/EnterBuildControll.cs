using Engine.Data;
using Engine.EGUI;
using Engine.Logic.Load;
using Engine.Logic.Locations.Generator;
using Engine.Map;
using Engine.Scenes;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Контроллер входа в локацию
    /// </summary>
    public class EnterBuildControll : MonoBehaviour
    {

        #pragma warning disable IDE0044

        [SerializeField] private GameObject hintPrefab;
        [SerializeField] private GameObject button;

        private Generator.LocationInfo buildInfo;

        public void Hide()
        {
            button.SetActive(false);
        }

        public void Show(Generator.LocationInfo buildInfo)
        {
            button.SetActive(true);
            this.buildInfo = buildInfo;
        }

        /// <summary>
        /// Игрок нажал на кнопку входа в локацию
        /// </summary>
        public void DoClick()
        {
            if (Game.Instance.Character.State.Health <= 10 // Проверяем, хватает ли у персонажа здоровье
                || Game.Instance.Character.State.Infection >= 90) // Персонаж не должен быть сильно инфецирован
            {
                var character = ObjectFinder.Find<MapCharacter>();
                UIHintMessageManager.Show(hintPrefab, character.transform.position, Localization.Instance.Get("msg_error_cant_enter_to_location"));
                return;
            }

            var load = ObjectFinder.Find<SceneToNextSceneLoadProcessor>();
            load.ShowLoad(SceneName.Build);
            load.SetDescription(Localization.Instance.Get("ui_map_load_location"));

            SaveContextToMemory();
            SceneManager.Instance.Switch(SceneName.Build);
        }

        private void SaveContextToMemory()
        {
            Debug.Log("save global map state to memory...");

            var character = ObjectFinder.Find<MapCharacter>();
            var human     = ObjectFinder.Find<MapHuman>();
            var runtime   = Game.Instance.Runtime;

            runtime.PlayerPosition    = human.transform.localPosition;
            runtime.CharacterPosition = character.transform.localPosition;
            runtime.PlayerContext     = human.MoveContext;
            runtime.CharacterContext  = character.MoveContext;

            runtime.Location = buildInfo;
            runtime.GenerationInfo = LocationGenerateContex.Generate(buildInfo);
        }

    }

}
