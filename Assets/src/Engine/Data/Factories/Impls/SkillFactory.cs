using System;
using System.Collections.Generic;

namespace Engine.Data.Factories
{

    public static class SkillsDictionary
    {
        public const string SKILL = "";

        public static class Scrap
        {

            public const string SCRAP_DIFFICULTY_1 = "ScrapDifficulty1";
            public const string SCRAP_DIFFICULTY_2 = "ScrapDifficulty2";
            public const string SCRAP_DIFFICULTY_3 = "ScrapDifficulty3";
            public const string SCRAP_DIFFICULTY_4 = "ScrapDifficulty4";

        }
        
    }

    public class SkillFactory
    {

        private static readonly Lazy<SkillFactory> instance = new Lazy<SkillFactory>(() => new SkillFactory());
        public static SkillFactory Instance { get { return instance.Value; } }
        
        private readonly IDictionary<string, ISkill> dataByID = new Dictionary<string, ISkill>();

        #region Ctor

        private SkillFactory()
        {
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_1, "scrap_difficulty_1_name", "scrap_difficulty_1_desc");
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_2, "scrap_difficulty_2_name", "scrap_difficulty_2_desc");
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_3, "scrap_difficulty_3_name", "scrap_difficulty_3_desc");
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_4, "scrap_difficulty_4_name", "scrap_difficulty_4_desc");
        }

        private void AddSkill(string id, string name, string description)
        {
            dataByID.Add(id, new Skill()
            {
                ID = id,
                Name = name,
                Description = description,
            });
        }

        #endregion

        public ISkill Get(string id)
        {
            return dataByID[id];
        }

    }

}
