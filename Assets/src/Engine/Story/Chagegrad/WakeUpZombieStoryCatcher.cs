using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class WakeUpZombieStoryCatcher : StoryBase
    {

        public override string StoryID => "main.chagegrad1.start_wakeup_zombie";

        [SerializeField] private CharacterNpcBehaviour zombie;
        [SerializeField] private WTLedBlinker blinker;
        
        public static bool Condition()
        {
            var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
            return quest.ContainsAllTags(ChagegradStartQuest.CheckPointMan,
                ChagegradStartQuest.CheckPointWindow,
                ChagegradStartQuest.CheckPointWomen);
        }

        public override void CreateDialog(DialogQueue dlg)
        {
            var pointExit = SelectVariant.Point;
            var pointWakeUp = SelectVariant.Point;
            dlg.IfGoTo(() => Condition(), pointWakeUp, pointExit);

            dlg.Point(pointWakeUp);
            dlg.Sound("quests/tutorial/zombie_wakeup", zombie.AttackAudioSource);
            dlg.Delay(2f);
            dlg.Run(() =>
            {
                blinker.Blink = true;
                
                Camera.main.SetState(PlayerEyePos, zombie.transform);
                zombie.Animator.SetCharacterDeadType(DeatType.Alive);
                zombie.Damaged.CanTakeDamage = true;
                zombie.CharacterContext.Status.State = CharacterStateType.Fighting;
                zombie.DeadEvent += () =>
                {
                    var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
                    quest.AddTag(ChagegradStartQuest.CheckPointKillZombie);
                    quest.Stage = 1;
                    ObjectFinder.Find<WomenDeadStoryCatcher>().RunDialog();
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
            dlg.Sound("tw/tw_01");
            dlg.Delay(1f);
            dlg.Music("mortal_kombat");
            dlg.Run(() =>
            {
                var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
                quest.AddTag(ChagegradStartQuest.CheckPointZombieWakeup);
            });
            dlg.Point(pointExit);
        }

        public override void FirstComplete()
        {
            base.FirstComplete();
            CheckBattle();
        }
        
        public override void SecondComplete()
        {
            base.SecondComplete();
            CheckBattle();
        }

        private void CheckBattle()
        {
            var quest = QuestFactory.Instance.Get<ChagegradStartQuest>();
            if (!Condition() || quest.ContainsTag(ChagegradStartQuest.CheckPointZombieWakeup))
                return;
            ObjectFinder.BattleManager.EnterToBattle();
        }
        
    }
    
}