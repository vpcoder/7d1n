using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class CorpseWomenStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private EnemyNpcBehaviour zombie;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, zombie);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag("Women");
            });
            dlg.Text("Женщина");
            
            WakeUpZombieStory.CheckWakeUp(dlg, zombie, PlayerEyePos);
        }

        protected override void EndDialogEvent()
        {
            base.EndDialogEvent();
            WakeUpZombieStory.EndProcessing();
        }
        
    }
    
}
