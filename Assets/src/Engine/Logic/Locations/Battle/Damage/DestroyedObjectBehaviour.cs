using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    public class DestroyedObjectBehaviour : DamagedBase
    {

        [SerializeField] private MeshExploder exploder;
        [SerializeField] private int health = 10;
        [SerializeField] private int protection = 0;
        [SerializeField] private long exp = 0;

        public override int Health
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

            exploder.Explode();
            GameObject.Destroy(gameObject);
        }

        public override int Protection { get { return protection; } set { this.protection = value; } }

        public override long Exp => exp;

    }

}
