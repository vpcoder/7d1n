using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Logic.Animations
{

    public class NpcDeadAnimationSet : StateMachineBehaviour
    {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Dead(animator);
        }

        private void Dead(Animator animator)
        {
            GameObject.Destroy(animator.gameObject.GetComponent<NpcDamagedBehaviour>());
            GameObject.Destroy(animator.gameObject.GetComponent<EnemyNpcBehaviour>());
            GameObject.Destroy(animator);
        }

    }

}
