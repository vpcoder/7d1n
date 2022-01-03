using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Объект который может получать урон
    /// </summary>
    [RequireComponent(typeof(EnemyItem))]
    public class NpcDamagedItem : MonoBehaviour, IDamagedObject
    {

        [SerializeField] protected AudioSource damageAudioSource;

        private EnemyItem enemyItem;

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
                return EnemyItem.Enemy.Exp;
            }
        }

        public EnemyItem EnemyItem
        {
            get
            {
                if(enemyItem == null)
                {
                    enemyItem = GetComponent<EnemyItem>();
                }
                return enemyItem;
            }
        }

        public int Health
        {
            get
            {
                return EnemyItem.Enemy.Health;
            }
            set
            {
                EnemyItem.Enemy.Health = value;
            }
        }

        public int Protection
        {
            get
            {
                return EnemyItem.Enemy.Protection;
            }
            set
            {
                EnemyItem.Enemy.Protection = value;
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
