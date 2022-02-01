using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public abstract class DamagedBase : MonoBehaviour, IDamagedObject
    {

        [SerializeField] protected AudioSource damageAudioSource;

        private EnemyNpcBehaviour currentNpc;

        public AudioSource DamageAudioSource { get { return damageAudioSource; } }

        public bool ExpGeted
        {
            get;
            set;
        } = false;

        public EnemyNpcBehaviour CurrentNPC
        {
            get
            {
                if (currentNpc == null)
                {
                    currentNpc = GetComponent<EnemyNpcBehaviour>();
                }
                return currentNpc;
            }
        }

        public abstract int Health { get; set; }
        public abstract int Protection { get; set; }
        public abstract long Exp { get; }

        public virtual void TakeDamage()
        {
            CurrentNPC.Animator.SetInteger(AnimationKey.DamageKey, 1);
            if (Health <= 0)
                CurrentNPC.Died();
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
