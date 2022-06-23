using System;
using System.Collections.Generic;

namespace Engine.Data.Factories
{

    public static class SkillsDictionary
    {
        public static class Scrap
        {
            public const string SCRAP_DIFFICULTY_1 = "ScrapDifficulty1"; // Навыки повышающие сложность разбора
            public const string SCRAP_DIFFICULTY_2 = "ScrapDifficulty2";
            public const string SCRAP_DIFFICULTY_3 = "ScrapDifficulty3";
            public const string SCRAP_DIFFICULTY_4 = "ScrapDifficulty4";

            
        }
    }

    /// <summary>
    ///
    /// Фабрика навыков
    /// Хранит все возможные навыки персонажа
    /// ---
    /// Skill Factory
    /// Stores all possible character skills
    /// 
    /// </summary>
    public class SkillFactory
    {

        #region Singleton
        
        private static readonly Lazy<SkillFactory> instance = new Lazy<SkillFactory>(() => new SkillFactory());
        public static SkillFactory Instance { get { return instance.Value; } }
        
        #endregion
        
        /// <summary>
        ///     Кэш навыков по их идентификатору
        ///     ---
        ///     Cache skills by their identifier
        /// </summary>
        private readonly IDictionary<string, ISkill> dataByID = new Dictionary<string, ISkill>();

        #region Ctor

        private SkillFactory()
        {
            InitSkills();
        }

        /// <summary>
        ///     Инициализирует коллекцию навыков
        ///     ---
        ///     Initializes a collection of skills
        /// </summary>
        private void InitSkills()
        {
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_1, "skill/scrap_difficulty_1_name", "skill/scrap_difficulty_1_desc");
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_2, "skill/scrap_difficulty_2_name", "skill/scrap_difficulty_2_desc");
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_3, "skill/scrap_difficulty_3_name", "skill/scrap_difficulty_3_desc");
            AddSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_4, "skill/scrap_difficulty_4_name", "skill/scrap_difficulty_4_desc");
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

        /// <summary>
        ///     Возвращает навык по его идентификатору
        ///     ---
        ///     Returns a skill by its identifier
        /// </summary>
        /// <param name="id">
        ///     Идентификатор навыка
        ///     ---
        ///     Skill Identifier
        /// </param>
        /// <returns>
        ///     Объект навыка
        ///     ---
        ///     Skill instance
        /// </returns>
        public ISkill Get(string id)
        {
            return dataByID[id];
        }

    }

}
