using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Story;
using Engine.Story.Chagegrad;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    /// <summary>
    ///
    /// Фабрика квестов
    /// Хранит все возможные квесты в мире
    /// ---
    /// Quest Factory
    /// Stores all possible quests in the world
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
        private readonly IDictionary<Type, IQuestInfo> dataByType = new Dictionary<Type, IQuestInfo>();

        /// <summary>
        ///     Кеш историй
        ///     Содержит мапу [История] -> [Число раз выполнения истории]
        ///     ---
        ///     Story cache
        ///     Contains the [Story] -> [Number of times story is executed]
        /// </summary>
        private readonly IDictionary<string, int> storyToCompleteCount = new Dictionary<string, int>(); 

        #region Ctor

        private QuestFactory()
        {
            foreach (var quest in AssembliesHandler.CreateImplementations<IQuestInfo>())
                dataByType.Add(quest.GetType(), quest);
        }
        
        #endregion
        
        public IQuestInfo Get(Type type)
        {
            return dataByType[type];
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public int GetStoryCount(string storyID)
        {
            if (!storyToCompleteCount.TryGetValue(storyID, out var count))
                return 0;
            return count;
        }

        public int GetStoryCount<T>(T story) where T : IStory
        {
            return GetStoryCount(story.StoryID);
        }

        public void IncStoryCount<T>(T story) where T : IStory
        {
            storyToCompleteCount[story.StoryID] = GetStoryCount(story) + 1;
        }

        public IEnumerable<QuestDataRepoObject> GetActiveQuests()
        {
            foreach (var quest in dataByType.Values)
                if (quest.State != QuestState.None)
                    yield return new QuestDataRepoObject()
                    {
                        Type = quest.GetType(),
                        State = (int)quest.State,
                        Stage = quest.Stage,
                        HashData = quest.HashData.ToList(),
                    };
        }
        
        public IEnumerable<StoryDataRepoObject> GetActiveStories()
        {
            foreach (var story in storyToCompleteCount)
                yield return new StoryDataRepoObject()
                {
                    StoryID = story.Key,
                    Count = story.Value,
                };
        }

        public void SetQuests(ICollection<QuestDataRepoObject> quests)
        {
            if(Lists.IsEmpty(quests))
                return;
            
            foreach (var serializedQuestData in quests)
            {
                var factoryQuestInfo = Get(serializedQuestData.Type);
                Set(factoryQuestInfo, serializedQuestData);
            }
        }

        public void SetStories(ICollection<StoryDataRepoObject> stories)
        {
            if(Lists.IsEmpty(stories))
                return;
            
            storyToCompleteCount.Clear();
            
            foreach (var story in stories)
                storyToCompleteCount.Add(story.StoryID, story.Count);
        }

        private void Set(IQuestInfo factoryQuestInfo, QuestDataRepoObject serializedQuestData)
        {
            factoryQuestInfo.Stage    = serializedQuestData.Stage;
            factoryQuestInfo.State    = (QuestState)serializedQuestData.State;
            factoryQuestInfo.HashData = serializedQuestData.HashData.ToSet();
        }

    }

}
