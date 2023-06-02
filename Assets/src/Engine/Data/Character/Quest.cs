using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data.Factories;

namespace Engine.Data
{
    
    [Serializable]
    public class StoryDataRepoObject
    {
        public string StoryID;
        public int Count;
    }
    
    [Serializable]
    public class QuestDataRepoObject
    {
        public Type Type;
        public int Stage;
        public int State;
        public List<string> HashData;
    }

    [Serializable]
    public class QuestRepositoryObject : IRepositoryObject
    {
        public long   ID { get { return IDValue; } set { } }
        public long   IDValue;
        
        public List<QuestDataRepoObject> Quests;
        public List<StoryDataRepoObject> Stories;
    }

    /// <summary>
    /// 
    /// Информация о квестах и историях
    /// ---
    /// Information about quests and stories
    /// 
    /// </summary>
    public class Quest : ICharacterStoredObjectSerializable<QuestRepositoryObject>
    {
        
        #region Serialization

        public QuestRepositoryObject CreateData()
        {
            var data = new QuestRepositoryObject
            {
                IDValue  = Game.Instance.Runtime.PlayerID,
                Quests   = QuestFactory.Instance.GetActiveQuests().ToList(),
                Stories  = QuestFactory.Instance.GetActiveStories().ToList(),
            };
            return data;
        }

        public void LoadFromData(QuestRepositoryObject data)
        {
            QuestFactory.Instance.SetQuests(data.Quests);
            QuestFactory.Instance.SetStories(data.Stories);
        }

        #endregion

    }

}
