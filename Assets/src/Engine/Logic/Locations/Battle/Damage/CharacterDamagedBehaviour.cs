using Engine.Character;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Объект который может получать урон
    /// </summary>
    public class CharacterDamagedBehaviour : DamagedBase
    {

        public override long Exp
        {
            get
            {
                return 50L;
            }
        }

        public override int Health
        {
            get
            {
                return Game.Instance.Character.State.Health;
            }
            set
            {
                Game.Instance.Character.State.Health = value;
                ObjectFinder.Find<CharacterStateHealth>().Value = value;
            }
        }

        public override int Protection
        {
            get
            {
                return Game.Instance.Character.State.Protection;
            }
            set
            {
                Game.Instance.Character.State.Protection = value;
                ObjectFinder.Find<CharacterStateProtection>().Value = value;
            }
        }

    }

}
