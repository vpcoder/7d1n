using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class CorpseWomenStoryCatcher : StorySelectCatcherBase
    {
        public override string StoryID => "main.chagedrad.start_corpse_women";

        [SerializeField] private CharacterNpcBehaviour zombie;
        [SerializeField] private Transform zombiePoint1;
        [SerializeField] private Transform zombiePoint2;
        [SerializeField] private WTLedBlinker blinker;
        
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
            
            WakeUpZombieStory.CheckWakeUp(dlg, blinker, zombie, PlayerEyePos);
        }

        public override void FirstComplete()
        {
            base.FirstComplete();
            WakeUpZombieStory.EndProcessing();
        }
        
        /// <summary>
        ///     Если история выполнялась, никогда не выполняем её более 1 раза
        ///     ---
        ///     If story has been run, never run it more than once
        /// </summary>
        public override bool SecondInit() { return false; }

        
    }
    
}
