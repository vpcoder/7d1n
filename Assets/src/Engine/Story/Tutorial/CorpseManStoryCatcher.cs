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
                Camera.main.SetState(ObjectFinder.Character.Eye, manPoint);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag("Man");
            });
            dlg.Text("");
            
            WakeUpZombieStory.CheckWakeUp(dlg, zombie);
        }
        
    }
    
}
