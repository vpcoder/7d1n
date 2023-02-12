using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WindowStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private Transform point3;

        public override void CreateDialog(DialogQueue dlg)
        {
            var camera = Camera.main;
            var fov = Camera.main.fieldOfView;
            var initCameraState = new TransformPair();

            dlg.Run(() =>
            {
                initCameraState = camera.GetState();
                camera.SetState(point1);
                camera.fieldOfView = 60f;
                camera.transform.LookAt(point2);
                ObjectFinder.Find<FloorSwitchController>().SetFloor(2);

                QuestFactory.Instance.Get<TutorialQuest>().AddTag("Window");
            });
            dlg.Text("Окна разбиты и закрыты всяким мусором");
            dlg.Run(() => StoryActionHelper.LookAt(camera, point3));
            dlg.Text("Сюда дует ветер, а за окном какая то разруха...");
            dlg.Text("Я в городе?");
            dlg.Run(() =>
            {
                Camera.main.fieldOfView = fov;
                Camera.main.transform.SetState(initCameraState);
                ObjectFinder.Find<FloorSwitchController>().SetFloor(1);
            });
            dlg.End();
        }

        protected override void EndDialogEvent()
        {
            
            base.EndDialogEvent();
        }
        
    }
    
}
