using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class MeetingStoryCatcher : StoryBase
    {
        
        public override string StoryID => "main.chagedrad.start_meeting";

        [SerializeField] private Transform cameraPoint;

        [SerializeField] private CharacterNpcBehaviour britt;
        [SerializeField] private CharacterNpcBehaviour laberius;
        [SerializeField] private CharacterNpcBehaviour immeral;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Text("test");

            dlg.Run(() =>
            {
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointMeeting);
            });
        }
        
        /// <summary>
        ///     Если история выполнялась, никогда не выполняем её более 1 раза
        ///     ---
        ///     If story has been run, never run it more than once
        /// </summary>
        public override bool SecondInit() { return false; }

    }
    
}
