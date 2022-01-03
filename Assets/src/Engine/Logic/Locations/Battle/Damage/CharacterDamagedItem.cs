using Engine.Character;
using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Объект который может получать урон
    /// </summary>
    public class CharacterDamagedItem : MonoBehaviour, IDamagedObject
    {

        [SerializeField] protected AudioSource damageAudioSource;

        public AudioSource DamageAudioSource { get { return damageAudioSource; } }

        public bool ExpGeted
        {
            get;
            set;
        } = false;

        public long Exp
        {
            get
            {
                return 50L;
            }
        }

        public int Health
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

        public int Protection
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

        public GameObject ToObject
        {
            get
            {
                return this.gameObject;
            }
        }

    }

}
