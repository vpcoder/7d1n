using Engine.Data.Factories;
using Engine.Data.Quests;
using Engine.Logic.Dialog;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class CorpseManStoryCatcher : StorySelectCatcherBase
    {

        [SerializeField] private Transform bloodPoint;
        [SerializeField] private Transform manPoint;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(ObjectFinder.Character.Eye, manPoint);
                QuestFactory.Instance.Get<TutorialQuest>().AddTag("Man");
            });
            
            dlg.Text("ваыва");
            dlg.End();
        }
        
    }
    
}
