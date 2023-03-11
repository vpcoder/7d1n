using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class CorpseManStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private EnemyNpcBehaviour zombie;
        [SerializeField] private Transform bloodPoint;
        [SerializeField] private Transform manPoint;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, manPoint);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag("Man");
            });
            
            dlg.Text("Мужчина... Он мёртв.");
            dlg.Text("Нет, я не медик, не подумай... Просто дыра в его груди... В неё без проблем войдёт моя рука.");
            dlg.Text("Взяв всё это в расчёт, можно сказать, что вряд ли до такого его довела семейная жизнь.");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, bloodPoint));
            });
            dlg.Text("Вряд ли его довела семейная жизнь");
            dlg.Text("Судя пов");
            dlg.Text("Что за чертовщина тут происходит?!");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, manPoint));
            });
            dlg.Text("...");

            WakeUpZombieStory.CheckWakeUp(dlg, zombie, PlayerEyePos);
        }
        
        protected override void EndDialogEvent()
        {
            base.EndDialogEvent();
            WakeUpZombieStory.EndProcessing();
        }
        
    }
    
}
