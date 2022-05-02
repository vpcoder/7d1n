
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// NPC который может получать урон
    /// ---
    /// NPC who can take damage
    /// 
    /// </summary>
    public class NpcDamagedBehaviour : NpcDamagedBase
    {

        /// <summary>
        ///     Опыт, который выдаётся за уничтожение этого существа или объекта
        ///     ---
        ///     The experience given for destroying that creature or object
        /// </summary>
        public override long Exp
        {
            get
            {
                return CurrentNPC.Enemy.Exp;
            }
        }

        /// <summary>
        ///     Здоровье/Состояние цели
        ///     ---
        ///     Health/target state
        /// </summary>
        public override int Health
        {
            get
            {
                return CurrentNPC.Enemy.Health;
            }
            set
            {
                CurrentNPC.Enemy.Health = value;
            }
        }

        /// <summary>
        ///     Защита цели
        ///     ---
        ///     Protecting the target
        /// </summary>
        public override int Protection
        {
            get
            {
                return CurrentNPC.Enemy.Protection;
            }
        }

    }

}
