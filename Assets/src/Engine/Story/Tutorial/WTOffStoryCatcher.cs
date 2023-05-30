using System.Collections.Generic;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WTOffStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private WTLedBlinker blinker;
        [SerializeField] private Transform cameraPoint;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            
            dlg.Run(() =>
            {
                Camera.main.SetState(cameraPoint, blinker.transform);
            });
            dlg.Text("Кажется звук идёт отсюда...");
            dlg.Text("Странная хреновина...");
            
            var acceptPoint = SelectVariant.Point;
            var resetPoint = SelectVariant.Point;
            var nextPoint = SelectVariant.Point;
            var list = new List<SelectVariant>()
            {
                SelectVariant.New("Принять вызов", acceptPoint),
                SelectVariant.New("Сбросить", resetPoint),
            };
            dlg.Select(list);

            dlg.Point(acceptPoint);
            dlg.Run(() =>
            {
                AudioController.Instance.StopMusic();
                blinker.Blink = false;
            });
            dlg.TextPlayer("- Алло...");
            dlg.Text("- Это Геннадий?");
            dlg.TextPlayer("- Нет...");
            dlg.Text("- А где Геннадий?");
            dlg.TextPlayer("- В душе не ебу где ваш Геннадий...");
            dlg.Text("[Шипение]");
            dlg.GoTo(nextPoint);
            
            dlg.Point(resetPoint);
            dlg.Run(() =>
            {
                AudioController.Instance.StopMusic();
                blinker.Blink = false;
            });
            dlg.Text("- Это Геннадий?");
            dlg.TextPlayer("- Так, я же сброс нажимал...");
            dlg.Text("- Какой сброс? Можно Геннадия пожалуйста?");
            dlg.TextPlayer("- В душе не ебу где ваш Геннадий...");
            dlg.Text("[Шипение]");

            dlg.Point(nextPoint);
            dlg.Text("Я не знаю что это было, но эта вещица может пригодиться");
            dlg.Run(() => blinker.gameObject.Destroy());
            dlg.Text("Может по этому звонить можно...");
            dlg.Run(() =>
            {
                QuestFactory.Instance.Get<TutorialQuest>().Stage = 2;
                var story = ObjectFinder.Find<ExitDoorStoryCatcher>();
                if (story != null)
                    story.IsActive = true;
            });
        }

    }
    
}
