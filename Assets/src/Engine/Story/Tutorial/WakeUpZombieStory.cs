using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WakeUpZombieStory
    {
        
        public static void CheckWakeUp(DialogQueue dlg, EnemyNpcBehaviour zombie)
        {
            dlg.Text("Что происходит?");
            dlg.Run(() =>
            {
                Camera.main.SetState(ObjectFinder.Character.Eye, zombie.transform);
                zombie.Animator.SetInteger(AnimationKey.DeadKey, 0);
                zombie.Agent.enabled = true;
            });

            dlg.End();
        }

    }
    
}