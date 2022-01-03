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

        public HashSet<string> SkillsData { get; set; } = new HashSet<string>(); // навыки персонажа
        
        public bool HasSkill(string id)
        {
            return SkillsData.Contains(id);
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
