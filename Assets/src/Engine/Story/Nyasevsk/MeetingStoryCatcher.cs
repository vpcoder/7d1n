using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using Engine.Logic.Dialog.Action.Impls;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Story.Nyasevsk
{
    
    public class MeetingStoryCatcher : StoryBase
    {

        public override string StoryID => "main.nyasevsk1.start_meeting";

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
                background.color = Color.white;
            });
            dlg.GlobalText("В помещении собираются люди...");
            dlg.Delay(0.5f);
            dlg.Run(() =>
            {
                brittBackupPos = britt.transform.GetState();
                laberiusBackupPos = laberius.transform.GetState();
                immeralBackupPos = immeral.transform.GetState();

                britt.Agent.enabled = false;
                britt.transform.SetState(brittPos);
                britt.Agent.enabled = true;
                
                laberius.Agent.enabled = false;
                laberius.transform.SetState(laberiusPos);
                laberius.Agent.enabled = true;
                
                immeral.Agent.enabled = false;
                immeral.transform.SetState(immeralPos);
                immeral.Agent.enabled = true;
                
                ObjectFinder.Character.transform.SetState(playerPos);
                ObjectFinder.Character.transform.LookAt(immeral.transform);
                
                camera.SetState(cameraPoint, immeral.transform);
            });

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

            if (Game.Instance.Character.Account.Gender == GenderType.Male)
            {
                dlg.TextAnother("- У нас тут совет, если ты ещё не понял что натворил, тебе сейчас нужно ждать решения и молчать в тряпочку, пока мы всё не обсудим.", immeral);
                dlg.TextAnother("- Этого варвара я буду слушать в самую последнюю очередь.", immeral);
            }
            else
            {
                dlg.TextAnother("- У нас тут совет, если ты ещё не поняла что натворила, тебе сейчас нужно ждать решения и молчать в тряпочку, пока мы всё не обсудим.", immeral);
                dlg.TextAnother("- Эту варваршу я буду слушать в самую последнюю очередь.", immeral);
            }
            
            dlg.TextPlayer("(Мерзкий тип)");
            
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.clear, Color.white, 0.8f));
            });
            dlg.Delay(1.5f);
            
            dlg.GlobalText("После долгих унижений всех участников совета Иммераль закончил собрание...");
            dlg.Text("...");
            dlg.GlobalText(null);
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f));
                StoryActionHelper.NpcGoTo(this.immeral, immeralBackupPos.position, true, MoveSpeedType.Walk);
                StoryActionHelper.NpcGoTo(this.laberius, laberiusBackupPos.position, true, MoveSpeedType.Walk);
                
                StoryActionHelper.NpcLookAt(this.britt, ObjectFinder.Character.Eye);
            });

            dlg.TextAnother("Иммераль вводит карантин на территории Нясевска. В город больше никто не войдёт, и не выйдет.", britt);
            dlg.Set("char", Game.Instance.Character.Account.Gender == GenderType.Male ? "парень" : "подруга");
            if (Game.Instance.Character.Account.Gender == GenderType.Male)
            {
                dlg.TextAnother("Возможно, парень, тебе не повезло, и ты действительно нам не врёшь, и являешься жертвой обстоятельств... Я надеюсь на это.", britt);
            }
            else
            {
                dlg.TextAnother("Возможно, подруга, тебе не повезло, и ты действительно нам не врёшь, и являешься жертвой обстоятельств... Я надеюсь на это.", britt);
            }
            dlg.TextAnother("Но время сейчас такое, что лучше осудить невиновного, чем поставить под угрозу мирных жителей Нясевска.", britt);
            dlg.TextAnother("У нас нет тюрем, поэтому следить за тобой будим самостоятельно.", britt);
            dlg.TextAnother("Кроме того, тебе запрещенно покидать пределы этого пригорода.", britt);
            dlg.TextAnother("На этом всё.", britt);
            dlg.Run(() =>
            {
                StoryActionHelper.NpcGoTo(this.britt, brittBackupPos.position, true, MoveSpeedType.Walk);
            });
            dlg.Delay(1f);
            dlg.Run(() =>
            {
                laberius.transform.SetState(laberiusBackupPos);
                immeral.transform.SetState(immeralBackupPos);
                QuestFactory.Instance.Get<NyasevskStartQuest>().AddTag(NyasevskStartQuest.CheckPointMeeting);
            });
        }
        
    }
    
}
