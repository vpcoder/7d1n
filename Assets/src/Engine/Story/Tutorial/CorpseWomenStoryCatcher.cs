using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class CorpseWomenStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private CharacterNpcBehaviour zombie;
        [SerializeField] private Transform zombiePoint1;
        [SerializeField] private Transform zombiePoint2;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, zombiePoint2);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag(TutorialQuest.CheckPointWomen);
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
            
            WakeUpZombieStory.CheckWakeUp(dlg, zombie, PlayerEyePos);
        }

        protected override void EndDialogEvent()
        {
            base.EndDialogEvent();
            WakeUpZombieStory.EndProcessing();
        }
        
    }
    
}
