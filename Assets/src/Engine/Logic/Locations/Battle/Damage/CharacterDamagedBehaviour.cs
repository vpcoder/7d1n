using Engine.Character;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Персонаж, получающий урон
    /// ---
    /// A character taking damage
    /// 
    /// </summary>
    public class CharacterDamagedBehaviour : NpcDamagedBase
    {

        /// <summary>
        ///     Опыт получаемый другими людьми за убийство персонажа игрока
        ///     ---
        ///     Experience gained by other people for killing a your character
        /// </summary>
        public override long Exp
        {
            get
            {
                return 50L;
            }
        }

        /// <summary>
        ///     Здоровье персонажа
        ///     ---
        ///     Health your character 
        /// </summary>
        public override int Health
        {
            get
            {
                return Game.Instance.Character.State.Health;
            }
            set
            {
                Game.Instance.Character.State.Health = value;
            }
        }

        /// <summary>
        ///     Защита персонажа
        ///     ---
        ///     Protecting your character
        /// </summary>
        public override int Protection
        {
            get
            {
                return CurrentCharacterCalculationService.CurrentProtection();
            }
        }

    }

}
