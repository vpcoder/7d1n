using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WakeUpZombieStory
    {

        private static bool Condition()
        {
            var quest = QuestFactory.Instance.Get<TutorialQuest>();
            return quest.ContainsAllTags("Man", "Window", "Women");
        }

        public static void EndProcessing()
        {
            if(!Condition())
                return;
            
            ObjectFinder.BattleManager.EnterToBattle();
        }
        
        public static void CheckWakeUp(DialogQueue dlg, EnemyNpcBehaviour zombie, Vector3 playerEyePos)
        {
            var pointExit = SelectVariant.Point;
            var pointWakeUp = SelectVariant.Point;
            dlg.IfGoTo(() => Condition(), pointWakeUp, pointExit);

            dlg.Point(pointWakeUp);
            dlg.Run(() =>
            {
                Camera.main.SetState(playerEyePos, zombie.transform);
                zombie.Animator.SetInteger(AnimationKey.DeadKey, 0);
            });
            dlg.Delay(0.5f, "Что...");
            dlg.Delay(0.5f, "Это...");
            dlg.Delay(0.5f, "За...");
            dlg.Text("Херня...");
            dlg.Run(() =>
            {
                zombie.Agent.enabled = true;
                zombie.CharacterContext.Status.State = NpcStateType.Fighting;
                zombie.DeadEvent += () =>
                {
                    ObjectFinder.Find<ExitDoorStoryCatcher>().enabled = true;
                };
            });
            dlg.Delay(1f);

            dlg.Point(pointExit);
        }

    }
    
}