using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class StartInTheBedStoryCatcher : StorySelectCatcherBase
    {
        
        public override string StoryID => "main.chagedrad.start_in_the_bed";
        
        [SerializeField] private Transform characterEyes;
        [SerializeField] private Transform leftWindow;
        [SerializeField] private Transform forwardWindow;
        [SerializeField] private Transform forwardWindow2;
        [SerializeField] private Transform rightSide;

        [SerializeField] private CharacterNpcBehaviour zombie;
        
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

            dlg.Delay(0.1f, "[.]");
            dlg.Delay(0.1f, "[..]");
            dlg.Delay(0.2f, "[...]");
            dlg.Delay(0.6f, "[Восстанавливаю нейронные связи]");
            dlg.Delay(0.4f, "[Восстанавливаю нейронные связи] - Ok!");
            dlg.Delay(0.6f, "[Возобновляю работу мозга]");
            dlg.Delay(0.4f, "[Возобновляю работу мозга] - Ok!");
            dlg.Delay(0.1f, "[.]");
            dlg.Delay(0.1f, "[..]");
            dlg.Delay(0.2f, "[...]");

            dlg.Delay(1f);
            
            dlg.Music("memories");
            dlg.Text("Этот шум...");
            dlg.Text("Что происходит?...");
            dlg.Text("Где я?...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Delay(2f);
            dlg.Text("Как давно я здесь?...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, forwardWindow2));
            });
            dlg.Text("Память спутана...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.white, 0.5f));
            });
            dlg.Text("Мне лишь хочется закрыть глаза и забыться...");
            dlg.Delay(1f);
            dlg.Text("...");
            dlg.Text("Я всё ещё здесь?");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Delay(2f);
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.white, 0.5f));
            });
            dlg.Text("Боль...");
            dlg.Text("Эта боль в конечностях сводит с ума...");
            dlg.Text("Мне нужно отдохнуть...");

            var point1 = SelectVariant.Point;
            var point2 = SelectVariant.Point;
            var nextPoint = SelectVariant.Point;
            var list = new List<SelectVariant> {
                SelectVariant.New("Сопротивляться сну", point1),
                SelectVariant.New("Лежать и пытаться набраться сил", point2)
            };
            dlg.Select(list);
            
            
            dlg.Point(point1);
            dlg.Text("Не могу, мне нужен сон...");

            var pointNext1 = SelectVariant.Point;
            list = new List<SelectVariant> {
                SelectVariant.New("Сопротивляться сну", pointNext1),
            };
            dlg.Select(list);

            dlg.Point(pointNext1);
            dlg.Text("...");
            
            var pointNext2 = SelectVariant.Point;
            list = new List<SelectVariant> {
                SelectVariant.New("Нужно встать", pointNext2),
            };
            dlg.Select(list);
            dlg.Point(pointNext2);
            
            dlg.Text("Хватит...");
            
            var pointNext3 = SelectVariant.Point;
            list = new List<SelectVariant> {
                SelectVariant.New("Вставай", pointNext3),
            };
            dlg.Select(list);
            dlg.Point(pointNext3);
            
            dlg.Text("Блять. Кто нажимает на эти сраные кнопки, сколько можно?!");
            dlg.Run(() =>
            {
                Game.Instance.Character.Account.AddPlayerRating(-1);
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.5f));
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
            dlg.Sound("quests/tutorial/zombie_talk", zombie.AttackAudioSource);
            dlg.GoTo(nextPoint);
            
            
            dlg.Point(point2);
            dlg.Text("Не хочется думать...");
            dlg.Delay(1f);
            dlg.Sound("dialogs/tutorial/dead", zombie.AttackAudioSource);
            
            dlg.Text("...");
            dlg.Run(() =>
            {
                Game.Instance.Character.Account.AddPlayerRating(1);
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, half, 0.5f));
            });
            dlg.Delay(1f);
            dlg.Text("?!");
            dlg.Sound("dialogs/tutorial/bed_1");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.LookAt(camera, rightSide));
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, half, Color.clear, 0.5f));
            });
            
            dlg.Point(nextPoint);
            dlg.Delay(1f);
            dlg.Text("Что за...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.clear, Color.white, 0.7f));
            });
            
            dlg.Sound("dialogs/tutorial/bed_3");
            dlg.Delay(2f);
            dlg.Run(() =>
            {
                PlayerCharacter.SetActive(true);
                PlayerCharacter.GetComponent<LocationCharacter>().MeshSwitcher.MeshIndex = Game.Instance.Character.Account.SpriteID;
                
                // Не добавляем RuntimeObjectList, чтобы скрипт доиграл до конца гарантированно
                // Do not add RuntimeObjectList, so that the script is guaranteed to finish
                StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f);
                
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointCharacterWakeup);
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