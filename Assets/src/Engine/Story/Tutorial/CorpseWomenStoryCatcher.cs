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
            
            
            WakeUpZombieStory.CheckWakeUp(dlg, zombie);
        }
        
    }
    
}
