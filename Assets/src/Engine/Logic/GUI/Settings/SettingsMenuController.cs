using Engine.Data;
using Engine.Data.Stories;
using Engine.EGUI;
using Engine.Scenes;

namespace Engine.Logic
{

    public class SettingsMenuController : Panel
    {

        public void OnExitClick()
        {
            CharacterStory.Instance.SaveAll(Game.Instance.Character);
            SceneManager.Instance.Switch(SceneName.Menu);
        }

        public void OnSettingsClick()
        {

        }

    }

}
