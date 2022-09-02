using Engine.Data;

namespace Engine
{

    /// <summary>
    ///
    /// Сервис расчётов опыта персонажей
    /// ---
    /// Character Experience Calculation Service
    /// 
    /// </summary>
    public static class ExpCalculationService
    {

        /// <summary>
        ///     Добавление единиц опыта
        ///     ---
        ///     Adding experience units
        /// </summary>
        /// <param name="value">
        ///     Количество добавляемого опыта
        ///     ---
        ///     Amount of experience to be added
        /// </param>
        /// <param name="field">
        ///     Поле опыта для которого происходит добавление
        ///     ---
        ///     The field of experience for which the addition takes place
        /// </param>
        /// <param name="mainField">
        ///     Поле общего опыта
        ///     ---
        ///     Field of Common Experience
        /// </param>
        public static void AddExp(long value, ExpField field, ExpField mainField)
        {
            AddExp(value, field);
            AddExp(value, mainField);
        }

        /// <summary>
        ///     Добавление единиц опыта
        ///     ---
        ///     Adding Experience Units
        /// </summary>
        /// <param name="value">
        ///     Количество добавляемого опыта
        ///     ---
        ///     Amount of experience to be added
        /// </param>
        /// <param name="field">
        ///     Поле опыта для которого происходит добавление
        ///     ---
        ///     The field of experience for which the addition takes place
        /// </param>
        private static void AddExp(long value, ExpField field)
        {
            if (field == null || value <= 0)
                return;

            field.Experience += value;

            if (field.Experience > field.MaxExperience) {
                var levelUp = field.Experience / field.MaxExperience;
                field.Experience %= field.MaxExperience;
                field.Level += levelUp;
                field.Points += levelUp;
            }
        }

    }

}
