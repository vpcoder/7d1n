using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data.Repositories;
using Engine.Story;
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
        private readonly IDictionary<string, IQuestInfo> dataByType = new Dictionary<string, IQuestInfo>();

        /// <summary>
        ///     Кеш историй
        ///     Содержит мапу [История] -> [Информация о истории]
        ///     ---
        ///     Story cache
        ///     Contains the [Story] -> [Story Information]
        /// </summary>
        private readonly IDictionary<string, StoryDataRepoObject> storyToInfo = new Dictionary<string, StoryDataRepoObject>();

        /// <summary>
        ///     Все истории в текущей сцене
        ///     ---
        ///     
        /// </summary>
        private readonly List<IStory> stories = new List<IStory>();

        #region Ctor

        private QuestFactory()
        {
            foreach (var quest in AssembliesHandler.CreateImplementations<IQuestInfo>())
                dataByType.Add(quest.GetType().FullName, quest);
        }
        
        #endregion

        public void RegisterStory(IStory story)
        {
            stories.Add(story);
        }

        public IEnumerable<IStory> GetStories()
        {
            if (Lists.IsEmpty(stories))
                yield break;
            
            for (int i = stories.Count - 1; i >= 0; i--)
            {
                if (stories[i] == null)
                {
                    stories.RemoveAt(i);
                    continue;
                }
                yield return stories[i];
            }
        }
        
        public IQuestInfo Get(string questID)
        {
            return dataByType[questID];
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T).FullName);
        }

        public StoryDataRepoObject GetStoryInfo(string storyID)
        {
            if (!storyToInfo.TryGetValue(storyID, out var info))
            {
                info = new StoryDataRepoObject();
                info.StoryID = storyID;
                info.Count = 0;
                info.IsActive = false;
                storyToInfo.Add(storyID, info);
            }
            return info;
        }

        public StoryDataRepoObject GetStoryInfo<T>(T story) where T : IStory
        {
            return GetStoryInfo(story.StoryID);
        }

        public void UpdateStoryInfo()
        {
            CharacterRepository.Instance.QuestRepository.Save(Game.Instance.Character.Quest.CreateData());
        }

        public IEnumerable<QuestDataRepoObject> GetActiveQuests()
        {
            foreach (var quest in dataByType.Values)
                if (quest.State != QuestState.None)
                    yield return new QuestDataRepoObject()
                    {
                        QuestID = quest.GetType().FullName,
                        State = (int)quest.State,
                        Stage = quest.Stage,
                        HashData = quest.HashData.ToList(),
                    };
        }
        
        public IEnumerable<StoryDataRepoObject> GetActiveStories()
        {
            foreach (var story in storyToInfo)
                yield return story.Value;
        }

        public void SetQuests(ICollection<QuestDataRepoObject> quests)
        {
            if(Lists.IsEmpty(quests))
                return;
            
            foreach (var serializedQuestData in quests)
            {
                var factoryQuestInfo = Get(serializedQuestData.QuestID);
                Set(factoryQuestInfo, serializedQuestData);
            }
        }

        public void SetStories(ICollection<StoryDataRepoObject> stories)
        {
            if(Lists.IsEmpty(stories))
                return;
            
            storyToInfo.Clear();
            
            foreach (var story in stories)
                storyToInfo.Add(story.StoryID, story);
        }

        private void Set(IQuestInfo factoryQuestInfo, QuestDataRepoObject serializedQuestData)
        {
            factoryQuestInfo.Stage    = serializedQuestData.Stage;
            factoryQuestInfo.State    = (QuestState)serializedQuestData.State;
            factoryQuestInfo.HashData = serializedQuestData.HashData.ToSet();
        }

    }

}
