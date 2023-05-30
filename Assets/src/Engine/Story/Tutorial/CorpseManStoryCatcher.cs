using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class CorpseManStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private CharacterNpcBehaviour zombie;
        [SerializeField] private Transform bloodPoint;
        [SerializeField] private Transform manPoint;
        [SerializeField] private WTLedBlinker blinker;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, manPoint);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag(TutorialQuest.CheckPointMan);
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
            dlg.Text("...");
            
            WakeUpZombieStory.CheckWakeUp(dlg, blinker, zombie, PlayerEyePos);
        }
        
        protected override void EndDialogEvent()
        {
            base.EndDialogEvent();
            WakeUpZombieStory.EndProcessing();
        }
        
    }
    
}
