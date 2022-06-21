using System;
using System.Collections.Generic;

namespace Engine.Data
{

    [Serializable]
    public class SkillsStoryObject : IStoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public HashSet<string> SkillsData;
    }

    
	public class Skills : ICharacterStoredObjectSerializable<SkillsStoryObject>
    {

        public HashSet<string> SkillsData { get; set; } = new HashSet<string>(); // Навыки персонажа
        
        public bool HasSkill(string skillId)
        {
            return SkillsData.Contains(skillId);
        }

        public bool AddSkill(string skillId)
        {
            return SkillsData.Add(skillId);
        }

        #region Serialization

        public SkillsStoryObject CreateData()
        {
            var data = new SkillsStoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                SkillsData = SkillsData
            };
            return data;
        }

        public void LoadFromData(SkillsStoryObject data)
        {
            this.SkillsData = data.SkillsData;
        }

        #endregion

    }

}
