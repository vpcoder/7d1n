using System.Collections.Generic;
using Engine.Data.Factories;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class StartInTheBedStory : StoryOnStart
    {
        
        [SerializeField] private Transform characterEyes;
        [SerializeField] private Transform leftWindow;
        [SerializeField] private Transform forwardWindow;
        [SerializeField] private Transform forwardWindow2;
        [SerializeField] private Transform rightSide;

        private Color half = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        
        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var camera = Camera.main;
            var fov = Camera.main.fieldOfView;
            var initCameraState = new TransformPair();
            
            dlg.Run(() =>
            {
                initCameraState = camera.GetState();
                camera.SetState(characterEyes.transform);
                camera.fieldOfView = 60f;
                ObjectFinder.Find<FloorSwitchController>().SetFloor(2);
                
                background.sprite = BackgroundFactory.Instance.GetRaw("UI/Base/black");
                background.color = Color.white;
            });
            dlg.Text("Что происходит?");
            dlg.Text("Где я?");
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, half, 0.5f);
            });
            dlg.Delay(2f, true);
            dlg.Text("Как давно я здесь?");
            dlg.Run(() =>
            {
                StoryActionHelper.LookAt(camera, forwardWindow2);
            });
            dlg.Text("Память спутана");
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, half, Color.white, 0.5f);
            });
            dlg.Text("Мне лишь хочется закрыть глаза и забыться...");
            dlg.Text("...");
            dlg.Text("Я всё ещё здесь?");
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, half, 0.5f);
            });
            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, half, Color.white, 0.5f);
            });
            dlg.Text("Боль...");
            dlg.Text("Эта боль в конечностях сводит с ума...");
            dlg.Text("Нужно ли сопротивляться, или поддаваться соблазну забвения?");

            var point1 = SelectVariant.Point;
            var point2 = SelectVariant.Point;
            var point3 = SelectVariant.Point;
            var list = new List<SelectVariant> {
                SelectVariant.New("Сопротивляться сну", point1),
                SelectVariant.New("Лежать и пытаться набраться сил", point2)
            };
            dlg.Select(list);
            
            
            dlg.Point(point1);
            dlg.Text("Мне нужно открыть глаза...");
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, half, 0.5f);
            });
            dlg.Text("...");
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, half, Color.clear, 0.5f);
            });
            dlg.Text("Сколько времени прошло?");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                StoryActionHelper.LookAt(camera, leftWindow);
            });
            dlg.Text("Не понимаю, что это за место...");
            dlg.Sound("dialogs/tutorial/bed_2");
            dlg.Run(() =>
            {
                StoryActionHelper.LookAt(camera, forwardWindow);
            });
            dlg.Text("...");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                StoryActionHelper.LookAt(camera, rightSide);
            });
            dlg.GoTo(point3);
            
            
            dlg.Point(point2);
            dlg.Text("Не хочется думать...");
            dlg.Text("...");
            dlg.Sound("dialogs/tutorial/dead");
            
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, half, 0.5f);
            });
            dlg.Text("???");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                StoryActionHelper.LookAt(camera, rightSide);
                StoryActionHelper.Fade(background, half, Color.clear, 0.5f);
            });
            
            
            dlg.Point(point3);
            dlg.Text("Что за...");
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.clear, Color.white, 0.7f);
            });
            dlg.Sound("dialogs/tutorial/bed_3");
            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f);
                
                Camera.main.fieldOfView = fov;
                Camera.main.transform.SetState(initCameraState);
                ObjectFinder.Find<FloorSwitchController>().SetFloor(1);
            });
            
        }

    }
    
}