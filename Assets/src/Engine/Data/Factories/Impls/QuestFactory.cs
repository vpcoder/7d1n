using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    /// <summary>
    ///
    /// Фабрика квестов
    /// Хранит все возможные квесты в мире
    /// ---
    /// 
    /// 
    /// </summary>
    public class QuestFactory
    {

        #region Singleton
        
        private static readonly Lazy<QuestFactory> instance = new Lazy<QuestFactory>(() => new QuestFactory());
        public static QuestFactory Instance { get { return instance.Value; } }
        
        #endregion
        
        /// <summary>
        ///     Кэш квестов по их типу
        ///     ---
        ///     Cache quests by their type
        /// </summary>
        private readonly IDictionary<Type, QuestInfo> dataByType = new Dictionary<Type, QuestInfo>();

        #region Ctor

        private QuestFactory()
        {
           
        }
        
        #endregion
        
        public QuestInfo Get(Type type)
        {
            return dataByType[type];
        }

        public QuestInfo Get<T>()
        {
            return Get(typeof(T));
        }

        public IEnumerable<QuestInfo> GetActiveQuests()
        {
            foreach (var quest in dataByType.Values)
                if (quest.State != QuestState.None)
                    yield return quest;
        }

        public void SetQuests(ICollection<QuestInfo> quests)
        {
            if(Lists.IsEmpty(quests))
                return;
            
            foreach (var serializedQuestData in quests)
            {
                var factoryQuestInfo = Get(serializedQuestData.GetType());
                Set(factoryQuestInfo, serializedQuestData);
            }
        }

        private void Set(QuestInfo factoryQuestInfo, QuestInfo serializedQuestData)
        {
            factoryQuestInfo.Stage    = serializedQuestData.Stage;
            factoryQuestInfo.State    = serializedQuestData.State;
            factoryQuestInfo.HashData = serializedQuestData.HashData;
        }

    }

}
