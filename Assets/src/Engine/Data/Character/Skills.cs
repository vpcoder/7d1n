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

        private ISet<string> skills { get; set; } = new HashSet<string>(); // Навыки персонажа
        
        public bool HasSkill(string skillId)
        {
            return skills.Contains(skillId);
        }

        public bool AddSkill(string skillId)
        {
            var result = skills.Add(skillId);
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
                SkillsData = skills.ToList()
            };
            return data;
        }

        public void LoadFromData(SkillsStoryObject data)
        {
            skills = data.SkillsData.ToSet();
        }

        #endregion

    }

}
