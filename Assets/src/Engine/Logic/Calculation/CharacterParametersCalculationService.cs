using System;
using Engine.Data;
using Engine.Logic;

namespace Engine
{

    /// <summary>
    ///
    /// Сервис расчётов параметров персонажей
    /// ---
    /// Service for calculating character parameters
    /// 
    /// </summary>
    public static class CharacterParametersCalculationService
    {

        /// <summary>
        ///     Добавление единиц параметра
        ///     ---
        ///     Adding parameter units
        /// </summary>
        /// <param name="type">
        ///     Параметр который нужно изменить
        ///     ---
        ///     Parameter to change
        /// </param>
        /// <param name="value">
        ///     Количество добавляемого параметра
        ///     ---
        ///     Number of parameters to be added
        /// </param>
        public static void AddParameter(StateFieldType type, int value)
        {
            switch (type)
            {
                case StateFieldType.Agility:
                    Game.Instance.Character.Parameters.Agility+=value;
                    break;
                case StateFieldType.Endurance:
                    Game.Instance.Character.Parameters.Endurance+=value;
                    Game.Instance.Character.State.MaxHealth+=value;
                    Game.Instance.Character.State.Health+=value;
                    break;
                case StateFieldType.Intellect:
                    Game.Instance.Character.Parameters.Intellect+=value;
                    break;
                case StateFieldType.Strength:
                    Game.Instance.Character.Parameters.Strength+=value;
                    Game.Instance.Character.State.MaxWeight+=value;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

    }

}
