using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class CorpseManStoryCatcher : StorySelectCatcherBase
    {
        public override string StoryID => "main.chagegrad.start_corpse_man";

        [SerializeField] private Transform bloodPoint;
        [SerializeField] private Transform manPoint;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, manPoint);
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointMan);
            });
            
            dlg.Text("Мужчина... Он мёртв.");
            dlg.Text("Я, конечно, не медик, но дыра в его груди... В неё без проблем войдёт моя рука.");
            dlg.Text("Вряд ли до такого его довела семейная жизнь...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, bloodPoint));
            });
            dlg.Text("Что за чертовщина тут происходит?");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, manPoint));
            });
            dlg.Text("Может из него вылез чужой?");
            dlg.Delay(1f);
            dlg.Text("...");
            
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
