using Engine.Data;
using Engine.Logic;
using UnityEngine;

namespace Engine.Scenes
{

    public enum SceneName
    {
        Menu,
        Map,
        Location,
    };

    public class SceneManager
    {

        public static SceneManager Instance = new SceneManager();

        public void Switch(SceneName scene)
        {
            var runtime = Game.Instance.Runtime;

            if(runtime.Scene == SceneName.Location) // Текущая сцена - локация?
                ObjectFinder.Find<LocationSaver>().SaveLocation(Game.Instance.Runtime.Location); // Сохраняем состояние сцены в хранилище

            runtime.Mode = Mode.Switch;

            ObjectFinder.Clear();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

    }

}
