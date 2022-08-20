using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;

namespace Engine
{
    
    /// <summary>
    /// 
    /// Сервис рассчёта разбора объектов на части
    /// ---
    /// Service for calculating the breakdown of objects into parts
    /// 
    /// </summary>
    public static class ScrapCalculationService
    {

        /// <summary>
        ///     Выполняет рассчёт - сколько ресурса извлечёт персонаж из этой части?
        ///     ---
        ///     Performs the calculation - how much resource will the character extract from this part?
        /// </summary>
        /// <param name="part">
        ///     Часть из которой состоит объект, которую игрок пытается извлечь
        ///     ---
        ///     The part that makes up the object that the player is trying to retrieve
        /// </param>
        /// <returns>
        ///     Возвращает количество извлечённого ресурса
        ///     0 - если ничего извлечь не удалось
        ///     ---
        ///     Returns the amount of extracted resource
        ///     0 - if nothing could be extracted
        /// </returns>
        public static long CalcResourceCount(Part part)
        {
            var maxPercentage = CalcMaxExtractPercentage(part);
            if (maxPercentage == 0)
                return 0;
            var maxCount = part.ResourceCount;
            var currPercentage = Random.Range(maxPercentage / 2, maxPercentage) / 100f;
            return (long)(currPercentage * maxCount);
        }

        /// <summary>
        ///     Высчитывает максимальное количество ресурсов, которые персонаж может получить при разборе указанной части объекта
        ///     ---
        ///     Calculates the maximum amount of resources that a character can get when he/she disassembles the specified part of the object
        /// </summary>
        /// <param name="part">
        ///     Часть из которой состоит объект, которую игрок пытается извлечь
        ///     ---
        ///     The part that makes up the object that the player is trying to retrieve
        /// </param>
        /// <returns>
        ///     Процент от 5 до 100, зависящий от навыков разбора и уровня разбора
        ///     ---
        ///     Percentages from 5 to 100, depending on parse skills and level of parse
        /// </returns>
        public static int CalcMaxExtractPercentage(Part part)
        {
            var partDifficulty = part.Difficulty;
            var characterDifficulty = GetCharacterScrapDifficulty();

            if (characterDifficulty < partDifficulty) // Персонажу недостаточно навыков чтобы получить хоть что то с этой части
                return 0;

            var lvl = 5 + (int)Game.Instance.Character.Exps.ScrapExperience.level;
            return lvl < 100 ? lvl : 100;
        }

        /// <summary>
        ///     Общий уровень сложности разбора у персонажа, по навыкам разбора
        ///     ---
        ///     The overall level of difficulty of breakdown in a character, by breakdown skill
        /// </summary>
        /// <returns>
        ///     Числовой уровень максимальной сложности разбора, с которой справляется персонаж
        ///     ---
        ///     Numerical level of maximum parsing difficulty, which the character can handle
        /// </returns>
        public static long GetCharacterScrapDifficulty()
        {
            var skills = Game.Instance.Character.Skills;
            var value = 0;
            if (skills.HasSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_1))
                value++;
            if (skills.HasSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_2))
                value++;
            if (skills.HasSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_3))
                value++;
            if (skills.HasSkill(SkillsDictionary.Scrap.SCRAP_DIFFICULTY_4))
                value++;
            return value;
        }
        
    }
    
}