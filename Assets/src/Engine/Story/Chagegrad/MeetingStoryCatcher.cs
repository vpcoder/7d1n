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

        public override string StoryID => "main.chagegrad.start_meeting";

        [SerializeField] private Transform cameraPoint;

        [SerializeField] private CharacterNpcBehaviour britt;
        [SerializeField] private CharacterNpcBehaviour laberius;
        [SerializeField] private CharacterNpcBehaviour immeral;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            var camera = Camera.main;
            dlg.Run(() =>
            {
                background.color = Color.white;
            });

            dlg.Text("В помещении собираются люди...");
            dlg.Run(() =>
            {
                dlg.RuntimeObjectList.Add(StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f));
            });

            dlg.TextAnother("- Как это произошло? Куда вы все смотрели?!", immeral);
            dlg.TextAnother("- Дык они без сознания уже несколько дней лежали. Не сидеть же мне всё это время у кроватей...", laberius);
            dlg.TextAnother("- Сидеть! А как ты думал? Это чужаки, ещё и варварами оказались. Вот и получайте - два трупа и один убийца!", immeral);

            var accidentPoint = SelectVariant.Point;
            var selfdefensePoint = SelectVariant.Point;
            var nextPoint = SelectVariant.Point;
            var list = new List<SelectVariant>()
            {
                SelectVariant.New("Это была случайность!", accidentPoint),
                SelectVariant.New("Это была самооборона!", selfdefensePoint),
            };
            dlg.Select(list);

            dlg.Point(accidentPoint);
            dlg.TextPlayer("Это была случайность. ");
            dlg.TextAnother("Два трупа и один убийца!", immeral);
            
            dlg.Run(() =>
            {
                QuestFactory.Instance.Get<ChagegradStartQuest>().AddTag(ChagegradStartQuest.CheckPointMeeting);
            });
        }
        
    }
    
}
