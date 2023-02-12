using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WakeUpZombieStory : StoryBase
    {
        
        [SerializeField] private Transform characterEyes;
        [SerializeField] private Transform zombiePoint;

        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var camera = Camera.main;
            var fov = Camera.main.fieldOfView;
            var initCameraState = new TransformPair();
            var currentFloor = ObjectFinder.Find<FloorSwitchController>().CurrentFloor;
            
            dlg.Run(() =>
            {
                initCameraState = camera.GetState();
                camera.SetState(characterEyes.transform);
                camera.fieldOfView = 60f;
                ObjectFinder.Find<FloorSwitchController>().SetMaxFloor();
            });
            dlg.Text("Что происходит?");

            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f);
                
                Camera.main.fieldOfView = fov;
                Camera.main.transform.SetState(initCameraState);
                ObjectFinder.Find<FloorSwitchController>().SetFloor(currentFloor);
            });
            
        }

    }
    
}