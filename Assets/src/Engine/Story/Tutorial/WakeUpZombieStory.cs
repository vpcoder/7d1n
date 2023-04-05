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
            return quest.ContainsAllTags(TutorialQuest.CheckPointMan,
                TutorialQuest.CheckPointWindow,
                TutorialQuest.CheckPointWomen);
        }

        public static void EndProcessing()
        {
            if(!Condition())
                return;
            
            ObjectFinder.BattleManager.EnterToBattle();
        }
        
        public static void CheckWakeUp(DialogQueue dlg, CharacterNpcBehaviour zombie, Vector3 playerEyePos)
        {
            var pointExit = SelectVariant.Point;
            var pointWakeUp = SelectVariant.Point;
            dlg.IfGoTo(() => Condition(), pointWakeUp, pointExit);

            dlg.Point(pointWakeUp);
            dlg.Sound("quests/tutorial/zombie_wakeup", zombie.AttackAudioSource);
            dlg.Run(() =>
            {
                Camera.main.SetState(playerEyePos, zombie.Eye);
                zombie.Animator.SetCharacterDeadType(DeatType.Alive);
                zombie.Damaged.CanTakeDamage = true;
                zombie.CharacterContext.Status.State = CharacterStateType.Fighting;
                zombie.DeadEvent += () =>
                {
                    ObjectFinder.Find<ExitDoorStoryCatcher>().enabled = true;
                };
            });
            dlg.Delay(0.5f, "Что...");
            dlg.Delay(0.5f, "Что это...");
            dlg.Run(() =>
            {
                zombie.Agent.enabled = true;

            });
            dlg.Delay(0.5f, "Что это за...");
            dlg.Delay(1f, "Что это за хуйня?!");

            dlg.Point(pointExit);
        }

    }
    
}