using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WindowStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private Transform windowLeftPoint;
        [SerializeField] private Transform windowsRightPoint;

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(ObjectFinder.Character.Eye, windowLeftPoint);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag("Window");
            });
            dlg.Text("Окна разбиты и закрыты всяким мусором");
            dlg.Run(() => StoryActionHelper.LookAt(Camera.main, windowsRightPoint));
            dlg.Text("Сюда задувает ветер, а за окном какая то разруха...");
            dlg.Text("Я в... городе?");
            dlg.End();
        }

    }
    
}
