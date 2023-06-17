using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class WindowStoryCatcher : StorySelectCatcherBase
    {

        public override string StoryID => "main.chagegrad.start_window";
        
        [SerializeField] private Transform windowLeftPoint;
        [SerializeField] private Transform windowsRightPoint;

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, windowLeftPoint);
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointWindow);
            });
            dlg.Text("Окна разбиты и закрыты всяким мусором");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, windowsRightPoint));
            });
            dlg.Text("Сюда задувает ветер, а за окном какая то разруха...");
            dlg.Text("Я в... городе?");

            dlg.Run(() =>
            {
                // Если все ключевые условия не наступили, не продолжаем историю
                // If all the key conditions have not come to pass, do not continue the story
                if (!WakeUpZombieStoryCatcher.Condition())
                    NextStories.Clear();
            });
        }
        
    }
    
}
