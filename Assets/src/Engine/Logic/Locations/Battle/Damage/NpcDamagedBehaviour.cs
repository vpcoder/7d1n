
namespace Engine.Logic.Locations
{

    /// <summary>
    /// NPC который может получать урон
    /// </summary>
    public class NpcDamagedBehaviour : DamagedBase
    {

        public override long Exp
        {
            get
            {
                return CurrentNPC.Enemy.Exp;
            }
        }

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

        public override int Protection
        {
            get
            {
                return CurrentNPC.Enemy.Protection;
            }
            set
            {
                CurrentNPC.Enemy.Protection = value;
            }
        }

    }

}
