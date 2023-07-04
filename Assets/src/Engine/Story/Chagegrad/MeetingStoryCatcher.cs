using System.Collections.Generic;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class MeetingStoryCatcher : StoryBase
    {

        public override string StoryID => "main.chagegrad1.start_meeting";

        [SerializeField] private Transform cameraPoint;

        [SerializeField] private Transform brittPos;
        [SerializeField] private Transform laberiusPos;
        [SerializeField] private Transform immeralPos;
        [SerializeField] private Transform playerPos;
        
        [SerializeField] private CharacterNpcBehaviour britt;
        [SerializeField] private CharacterNpcBehaviour laberius;
        [SerializeField] private CharacterNpcBehaviour immeral;

        private TransformPair brittBackupPos;
        private TransformPair laberiusBackupPos;
        private TransformPair immeralBackupPos;

        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var camera = Camera.main;
            dlg.Run(() =>
            {
                brittBackupPos = brittPos.GetState();
                laberiusBackupPos = laberiusPos.GetState();
                immeralBackupPos = immeralPos.GetState();
                
                britt.transform.SetState(brittPos);
                laberius.transform.SetState(laberiusPos);
                immeral.transform.SetState(immeralPos);
                PlayerCharacter.transform.SetState(playerPos);
                
                camera.SetState(cameraPoint, immeral.transform);

                background.color = Color.white;
            });

            dlg.GlobalText("В помещении собираются люди...");
            dlg.Text("...");
            dlg.GlobalText(null);
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f));
            });

            dlg.TextAnother("- Как это произошло? Куда вы все смотрели?!", immeral);
            dlg.TextAnother("- Дык они без сознания уже несколько дней лежали. Не сидеть же мне всё это время у кроватей...", laberius);
            dlg.TextAnother("- Сидеть! А как ты думал? Это чужаки, что от них ещё ожидать? Вот и получайте - два трупа и один убийца!", immeral);
            
            var nextPoint = SelectVariant.Point;
            var list = new List<SelectVariant>()
            {
                SelectVariant.New("Это была случайность!", nextPoint),
                SelectVariant.New("Это была самооборона!", nextPoint),
            };
            dlg.Select(list);
            dlg.Point(nextPoint);

            dlg.TextAnother("- У нас тут совет, если ты ещё не понял что натворил, тебе сейчас нужно ждать решения и молчать в тряпочку, пока мы всё не обсудим.", immeral);
            dlg.TextAnother("- Этого варвара я буду слушать в самую последнюю очередь.", immeral);
            dlg.TextPlayer("(Мерзкий тип)");
            
            dlg.Run(() =>
            {
                
                britt.transform.SetState(brittBackupPos);
                laberius.transform.SetState(laberiusBackupPos);
                immeral.transform.SetState(immeralBackupPos);
                
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointMeeting);
            });
        }
        
    }
    
}
