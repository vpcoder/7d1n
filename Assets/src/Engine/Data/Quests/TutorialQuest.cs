using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Data.Quests
{
    
    public class TutorialQuest : QuestInfo
    {

        public const string CheckPointMan = "Man";
        public const string CheckPointWindow = "Window";
        public const string CheckPointWomen = "Women";
        
        private static readonly List<string> descriptionList = new List<string>()
        {
            "Кажется я пришёл в себя, но совершенно не понимаю где я, кто я, и что происходит вокруг... Нужно осмотреться...",
            "Во время осмотра произошёл казус, с одной дамой, в это же время на столе заиграло какое то устройство связи, похожее на рацию...",
            "Нужно искать выход из этого странного места, тут есть дверь.",
            "",
        };

        public TutorialQuest() : base() { }
        public TutorialQuest(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Name => "Этот новый мир";
        
        public override IList<string> getDescription()
        {
            return descriptionList;
        }
        
    }
    
}