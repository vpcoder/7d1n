using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data.Stories;

namespace Engine.Data
{

    [Serializable]
    public class SkillsStoryObject : IStoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        // List, потому что HashSet не сериализуемый
        public List<string> SkillsData;
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
            var result = SkillsData.Add(skillId);
            if(result)
                CharacterStory.Instance.SkillsStory.Save(CreateData());
            return result;
        }

        #region Serialization

        public SkillsStoryObject CreateData()
        {
            var data = new SkillsStoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                SkillsData = SkillsData.ToList()
            };
            return data;
        }

        public void LoadFromData(SkillsStoryObject data)
        {
            SkillsData = data.SkillsData.ToHashSet();
        }

        #endregion

    }

}
