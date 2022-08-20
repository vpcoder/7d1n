using Engine.Data;

namespace Engine
{

    /// <summary>
    /// Сервис расчётов опыта
    /// </summary>
    public static class ExpCalculationService
    {

        /// <summary>
        /// Добавление опыта
        /// </summary>
        /// <param name="value">Количество добавляемого опыта</param>
        /// <param name="field">Поле опыта для которого происходит добавление</param>
        /// <param name="mainField">Поле общего опыта</param>
        public static void AddExp(long value, ExpField field, ExpField mainField)
        {
            AddExp(value, field);
            AddExp(value, mainField);
        }

        /// <summary>
        /// Добавление опыта
        /// </summary>
        /// <param name="value">Количество добавляемого опыта</param>
        /// <param name="field">Поле опыта для которого происходит добавление</param>
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
