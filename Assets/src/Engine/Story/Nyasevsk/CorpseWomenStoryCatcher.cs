using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using UnityEngine;

namespace Engine.Story.Nyasevsk
{
    
    public class CorpseWomenStoryCatcher : StorySelectCatcherBase
    {
        public override string StoryID => "main.nyasevsk1.start_corpse_women";

        [SerializeField] private Transform zombiePoint1;
        [SerializeField] private Transform zombiePoint2;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, zombiePoint2);
                QuestFactory.Instance.Get<NyasevskStartQuest>().AddTag(NyasevskStartQuest.CheckPointWomen);
            });
            dlg.Text("Женщина, на вид молодая...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, zombiePoint1));
            });
            dlg.Text("На теле множество ран... Они перебинтованы и обработаны.");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, zombiePoint2));
            });
            dlg.Text("Похоже, её пытались вылечить...");
            
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
