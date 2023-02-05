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
    public class QuestStoryObject : IStoryObject
    {
        public long   ID { get { return IDValue; } set { } }
        public long   IDValue;
        
        public List<QuestInfo> Quests;
    }

    /// <summary>
    /// 
    /// Информация о квестах и историях
    /// ---
    /// 
    /// 
    /// </summary>
    public class Quest : ICharacterStoredObjectSerializable<QuestStoryObject>
    {
        
        #region Serialization

        public QuestStoryObject CreateData()
        {
            var data = new QuestStoryObject
            {
                IDValue  = Game.Instance.Runtime.PlayerID,
                Quests   = QuestFactory.Instance.GetActiveQuests().ToList(),
            };
            return data;
        }

        public void LoadFromData(QuestStoryObject data)
        {
            QuestFactory.Instance.SetQuests(data.Quests);
        }

        #endregion

    }

}
