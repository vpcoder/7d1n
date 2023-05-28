using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data.Repositories;

namespace Engine.Data
{

    [Serializable]
    public class SkillsRepositoryObject : IRepositoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        // List, потому что HashSet не сериализуемый
        public List<string> SkillsData;
        public List<long> BlueprintsData;
    }


    public class Skills : ICharacterStoredObjectSerializable<SkillsRepositoryObject>
    {

        /// <summary>
        ///     Множество навыков, которые уже есть у персонажа
        ///     ---
        ///     Set of skills that the character already has
        /// </summary>
        private ISet<string> skills { get; set; } = new HashSet<string>(); // Навыки персонажа

        /// <summary>
        ///     Множество чертежей, которые запомнил персонаж
        ///     ---
        ///     Set of blueprints that the character has memorized
        /// </summary>
        private ISet<long> blueprints { get; set; } = new HashSet<long>(); // Чертежи, которые запомнил персонаж

        public ISet<long> GetBlueprints()
        {
            return blueprints;
        }

        public bool AddBlueprint(long blueprintId)
        {
            var result = blueprints.Add(blueprintId);
            if(result)
                CharacterRepository.Instance.SkillsRepository.Save(CreateData());
            return result;
        }
        
        public bool HasBlueprint(long blueprintId)
        {
            return blueprints.Contains(blueprintId);
        }
        
        
        public bool HasSkill(string skillId)
        {
            return skills.Contains(skillId);
        }

        public bool AddSkill(string skillId)
        {
            var result = skills.Add(skillId);
            if(result)
                CharacterRepository.Instance.SkillsRepository.Save(CreateData());
            return result;
        }

        #region Serialization

        public SkillsRepositoryObject CreateData()
        {
            var data = new SkillsRepositoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                SkillsData = skills.ToList(),
                BlueprintsData = blueprints.ToList(),
            };
            return data;
        }

        public void LoadFromData(SkillsRepositoryObject data)
        {
            skills = data.SkillsData.ToSet();
            blueprints = data.BlueprintsData.ToSet();
        }

        #endregion

    }

}
