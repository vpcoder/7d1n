using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class WindowStoryCatcher : StorySelectCatcherBase
    {

        public override string StoryID => "main.chagedrad.start_window";
        
        [SerializeField] private CharacterNpcBehaviour zombie;
        [SerializeField] private Transform windowLeftPoint;
        [SerializeField] private Transform windowsRightPoint;
        [SerializeField] private WTLedBlinker blinker;

        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, windowLeftPoint);
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointWindow);
            });
            dlg.Text("Окна разбиты и закрыты всяким мусором");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(Camera.main, windowsRightPoint));
            });
            dlg.Text("Сюда задувает ветер, а за окном какая то разруха...");
            dlg.Text("Я в... городе?");

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
