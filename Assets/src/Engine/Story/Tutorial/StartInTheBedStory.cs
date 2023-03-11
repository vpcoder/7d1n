using System.Collections.Generic;
using Engine.Data;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class StartInTheBedStory : StorySelectCatcherBase
    {
        [SerializeField] private Transform characterEyes;
        [SerializeField] private Transform leftWindow;
        [SerializeField] private Transform forwardWindow;
        [SerializeField] private Transform forwardWindow2;
        [SerializeField] private Transform rightSide;

        [SerializeField] private List<StoryBase> stories;
        
        private Color half = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var camera = Camera.main;

            dlg.Run(() =>
            {
                camera.SetState(characterEyes, forwardWindow);
                background.color = Color.white;
            });
            
            dlg.Text("Что происходит?");
            
            dlg.Text("Где я?");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Delay(2f, true);
            dlg.Text("Как давно я здесь?");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, forwardWindow2));
            });
            dlg.Text("Память спутана");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.white, 0.5f));
            });
            dlg.Text("Мне лишь хочется закрыть глаза и забыться...");
            dlg.Text("...");
            dlg.Text("Я всё ещё здесь?");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.white, 0.5f));
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
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Text("...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.clear, 0.5f));
            });
            dlg.Text("Сколько времени прошло?");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, leftWindow));
            });
            dlg.Text("Не понимаю, что это за место...");
            dlg.Sound("dialogs/tutorial/bed_2");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, forwardWindow));
            });
            dlg.Text("...");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, rightSide));
            });
            dlg.GoTo(point3);
            
            
            dlg.Point(point2);
            dlg.Text("Не хочется думать...");
            dlg.Text("...");
            dlg.Sound("dialogs/tutorial/dead");
            
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Text("???");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, rightSide));
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.clear, 0.5f));
            });
            
            
            dlg.Point(point3);
            dlg.Text("Что за...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.clear, Color.white, 0.7f));
            });
            
            dlg.Sound("dialogs/tutorial/bed_3");
            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                PlayerCharacter.SetActive(true);
                PlayerCharacter.GetComponent<LocationCharacter>().MeshSwitcher.MeshIndex = Game.Instance.Character.Account.SpriteID;
                
                foreach (var story in stories)
                    story.enabled = true;
                
                StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f); // Не добавляем RuntimeObjectList, чтобы скрипт доиграл до конца гарантированно
                
                EndDialogEvent();
            });
        }

    }
    
}