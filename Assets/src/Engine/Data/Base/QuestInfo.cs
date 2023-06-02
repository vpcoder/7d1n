using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Engine.Data
{
    
    [Serializable]
    public abstract class QuestInfo : IQuestInfo
    {
        private QuestState state;
        private int stage;
        private ISet<string> data;

        public bool IsStarted => state == QuestState.Started;
        public bool IsCompleted => state == QuestState.Completed;
        
        public QuestState State
        {
            get { return state; }
            set { state = value; }
        }
        
        public int Stage 
        {
            get { return stage; }
            set
            {
                if (value <= 0)
                {
                    stage = 0;
                    return;
                }
                var descriptions = getDescription();
                if (descriptions.Count > value)
                {
                    stage = value;
                    return;
                }
                stage = descriptions.Count - 1;
            }
        }
        
        public ISet<string> HashData 
        {
            get { return data; }
            set { data = value; }
        }

        public abstract string Name { get; }

        public abstract IList<string> getDescription();

        public void AddTag(string tag)
        {
            HashData.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            HashData.Remove(tag);
        }

        public bool ContainsTag(string tag)
        {
            return HashData.Contains(tag);
        }
        
        public bool NotContainsTag(string tag)
        {
            return !ContainsTag(tag);
        }
        
        public bool NotContainsAllTags([NotNull] params string[] tags)
        {
            return !ContainsAllTags(tags);
        }
        
        public bool NotContainsAllTags([NotNull] ICollection<string> tags)
        {
            return !ContainsAllTags(tags);
        }
        
        public bool ContainsAllTags([NotNull] params string[] tags)
        {
            if (tags == null)
                return false;
            return ContainsAllTags(tags.ToList());
        }
        
        public bool ContainsAllTags([NotNull] ICollection<string> tags)
        {
            if (Lists.IsEmpty(tags))
                return false;
            foreach (var tag in tags)
            {
                if (NotContainsTag(tag))
                    return false;
            }
            return true;
        }
        
        public IEnumerable<string> getBeforeStageDescriptions()
        {
            if (stage == 0 || state == QuestState.None)
            {
                yield return null;
            }
            else
            {
                var descriptions = getDescription();
                for (int i = 0; i < stage; i++)
                    yield return descriptions[i];
            }
        }

        public string getCurrentStageDescription()
        {
            if (state == QuestState.None)
                return null;
            return getDescription()[stage];
        }

        public void NextStage()
        {
            Stage++;
        }

        public void Start()
        {
            State = QuestState.Started;
        }

        #region Serialization

        public QuestInfo()
        {
            stage = 0;
            state = QuestState.None;
            data = new HashSet<string>();
        }
        
        protected QuestInfo(SerializationInfo info, StreamingContext context)
        {
            state = (QuestState)info.GetInt32("state");
            stage = info.GetInt32("stage");
            data = ((List<string>)info.GetValue("data", typeof(List<string>)))?.ToSet() ?? new HashSet<string>();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("state", state);
            info.AddValue("stage", stage);
            info.AddValue("data", data?.ToList());
        }
        
        #endregion
        
    }
    
}
