using Engine.Data;
using Engine.Data.Stories;
using Engine.DB;
using Engine.Logic.Load;
using Engine.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class MenuActionsController : MonoBehaviour
    {

#pragma warning disable 0649, IDE0044

        [SerializeField] private SelectPlayerController selectPlayerController;
        [SerializeField] private GameObject buttonsPanel;
        [SerializeField] private Text txtMeta;

#pragma warning restore 0649, IDE0044

        private void Awake()
        {

#if PLATFORM_ANDROID
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
#endif

            Db.Instance.CheckDb();

            var playerID = GameSettings.Instance.Settings.PlayerID;
            if (playerID == -1)
            {
                selectPlayerController.Show();
            }
            else
            {
                Game.Instance.Runtime.PlayerID = playerID;
                CharacterStory.Instance.LoadAll(Game.Instance.Character);
            }

            txtMeta.text = DbConfigurator.CreateMeta();
        }

        public void OnStartClick()
        {
            var load = ObjectFinder.Find<SceneToNextSceneLoadProcessor>();
            load.ShowLoad(LoadBackgroundType.Map);

            buttonsPanel.SetActive(false);

            load.SetDescription(Localization.Instance.Get("ui_menu_load_playerdata"));
            CharacterStory.Instance.LoadAll(Game.Instance.Character);

            load.SetDescription(Localization.Instance.Get("ui_menu_load_mapscene"));
            SceneManager.Instance.Switch(SceneName.Map);
        }

        public void OnSettingsClick()
        {

        }

        public void OnSelectPlayerClick()
        {
            ObjectFinder.Find<SelectPlayerController>().Show();
        }

        public void OnExitClick()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.Exit(0);
#endif
        }

        public void OnClearDBClick()
        {
            for (int i = 0; i < 10; i++)
                CharacterStory.Instance.Delete(i);
            DbConfigurator.DoResetDB();
        }

    }

}
