using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class ExitDoorStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private Transform door;
        [SerializeField] private Vector3 openAngles;
        [SerializeField] private Transform lookTo;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, door);
                QuestFactory.Instance.Get<TutorialQuest>().Stage++;
            });
            dlg.Text("Кажется, тут не закрыто...");
            dlg.Run(() =>
            {
                door.transform.localRotation = Quaternion.Euler(openAngles);
            });
            
            dlg.Text("Выходим");
        }

    }
    
}