using System.Collections.Generic;

namespace Engine.Data
{
    public interface IQuestInfo
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