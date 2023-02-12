using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Data
{
    public interface IQuestInfo : ISerializable
    {
        QuestState State { get; set; }
        int Stage { get; set; }
        string Name { get; }
        ISet<string> HashData { get; set; }


        bool IsStarted { get; }
        bool IsCompleted { get; }

        IList<string> getDescription();
        IEnumerable<string> getBeforeStageDescriptions();
        string getCurrentStageDescription();
    }
}