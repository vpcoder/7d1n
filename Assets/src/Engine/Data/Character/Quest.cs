using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data.Factories;

namespace Engine.Data
{
    
    [Serializable]
    public class QuestData
    {
        public long QuestID;
        public QuestState State = QuestState.None;
        public int Stage;
        public List<string> HashData;
    }
    
    [Serializable]
    public class QuestRepositoryObject : IRepositoryObject
    {
        public long   ID { get { return IDValue; } set { } }
        public long   IDValue;
        
        public List<IQuestInfo> Quests;
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
            };
            return data;
        }

        public void LoadFromData(QuestRepositoryObject data)
        {
            QuestFactory.Instance.SetQuests(data.Quests);
        }

        #endregion

    }

}
