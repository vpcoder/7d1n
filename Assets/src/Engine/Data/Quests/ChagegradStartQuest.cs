using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Data.Quests
{
    
    public class ChagegradStartQuest : QuestInfo
    {

        public const string CheckPointMan = "Man";
        public const string CheckPointWindow = "Window";
        public const string CheckPointWomen = "Women";
        public const string CheckPointKillZombie = "KillZombie";
        public const string CheckPointZombieWakeup = "WakeupZombie";
        public const string CheckPointCharacterWakeup = "Wakeup";
        public const string CheckPointMeeting = "Meeting";
        
        private static readonly List<string> descriptionList = new List<string>()
        {
            "Кажется я пришёл в себя, но совершенно не понимаю где я, кто я, и что происходит вокруг... Нужно осмотреться...",
            "Во время осмотра произошёл казус, с одной дамой, в это же время на столе заиграло какое то устройство связи, похожее на рацию...",
            "Нужно искать выход из этого странного места, тут есть дверь.",
            "",
        };

        public ChagegradStartQuest() : base() { }
        public ChagegradStartQuest(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Name => "Этот новый мир";
        
        public override IList<string> getDescription()
        {
            return descriptionList;
        }
        
    }
    
}