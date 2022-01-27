using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    public class DestroyedObjectBehaviour : MonoBehaviour, IDamagedObject
    {

        [SerializeField] private MeshExploder exploder;
        [SerializeField] private int health = 10;
        [SerializeField] private int protection = 0;
        [SerializeField] private long exp = 0;

        private bool isDestroy = false;

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                this.health = value;
            }
        }

        public void CheckDead()
        {
            if (this.health > 0)
                return;

            isDestroy = true;

            exploder.Explode();
            GameObject.Destroy(gameObject);
        }

        public int Protection { get { return protection; } set { this.protection = value; } }

        public bool ExpGeted { get; set; }

        public long Exp => exp;

        public AudioSource DamageAudioSource => null;

        public GameObject ToObject => isDestroy ? null : gameObject;

    }

}
