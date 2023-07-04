using System.Collections.Generic;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class WTOffStoryCatcher : StorySelectCatcherBase
    {
        
        public override string StoryID => "main.chagegrad1.start_wt_off";

        [SerializeField] private WTLedBlinker blinker;
        [SerializeField] private Transform cameraPoint;
        [SerializeField] private Transform door;
        
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
            dlg.TextPlayer("- Ну, допустим, ало?...");
            dlg.Sound("tw/tw_01");
            dlg.Text("- Это Геннадий?");
            dlg.TextPlayer("- Нет...");
            dlg.Sound("tw/tw_02");
            dlg.Text("- А где Геннадий?");
            dlg.TextPlayer("- В душе не ебу где ваш Геннадий...");
            dlg.GoTo(nextPoint);
            
            dlg.Point(resetPoint);
            dlg.Run(() =>
            {
                AudioController.Instance.StopMusic();
                blinker.Blink = false;
            });
            dlg.Sound("tw/tw_01");
            dlg.Text("- Это Геннадий?");
            dlg.TextPlayer("- Так, я же сброс нажимал...");
            dlg.Sound("tw/tw_02");
            dlg.Text("- Какой сброс? Можно Геннадия пожалуйста?");
            dlg.TextPlayer("- В душе не ебу где ваш Геннадий...");
            
            dlg.Point(nextPoint);
            dlg.Sound("tw/tw_03");
            dlg.Text("...");
            dlg.Text("Я не знаю что это было, но эта вещица может пригодиться.");
            dlg.Run(() => blinker.gameObject.Destroy());
            dlg.Text("Может по этому говну звонить можно.");
            dlg.Text("Ну или орехи им колоть.");
            dlg.Run(() =>
            {
                QuestFactory.Instance.Get<ChagegradStartQuest>().Stage = 2;
            });
            
            dlg.Run(() =>
            {
                PlayerCharacter.SetActive(false);
                Camera.main.SetState(PlayerEyePos, door);
            });
            dlg.Text("Хм...");
            dlg.Text("Дверь значит...");
            dlg.Music("memories");
            dlg.Run(() =>
            {
                PlayerCharacter.SetActive(true);
            });
        }

        /// <summary>
        ///     Если история выполнялась, никогда не выполняем её более 1 раза
        ///     ---
        ///     If story has been run, never run it more than once
        /// </summary>
        public override bool SecondInit()
        {
            if(blinker != null)
                blinker.gameObject.Destroy();
            return false;
        }
        
    }
    
}
