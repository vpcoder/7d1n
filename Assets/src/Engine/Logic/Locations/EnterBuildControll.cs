using Engine.Data;
using Engine.EGUI;
using Engine.IO;
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
            if(Game.Instance.Character.State.Health <= 10 // Проверяем, хватает ли у персонажа здоровье
                || Game.Instance.Character.State.Infection >= 90) // Персонаж не должен быть сильно инфецирован
            {
                var character = ObjectFinder.Find<MapCharacter>();
                UIHintMessageManager.Show(hintPrefab, character.transform.position, Localization.Instance.Get("msg_error_cant_enter_to_location"));
                return;
            }

            var runtime = Game.Instance.Runtime;

            runtime.PlayerPosition = ObjectFinder.Find<MapHuman>().transform.localPosition;
            runtime.CharacterPosition = ObjectFinder.Find<MapCharacter>().transform.localPosition;
            runtime.PlayerContext = ObjectFinder.Find<MapHuman>().MoveContext;
            runtime.CharacterContext = ObjectFinder.Find<MapCharacter>().MoveContext;
            runtime.Location = buildInfo;

            SceneManager.Instance.Switch(SceneName.Location);
        }

    }

}
