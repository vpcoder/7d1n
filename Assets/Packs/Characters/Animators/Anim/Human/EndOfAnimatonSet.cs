using UnityEngine;

namespace Engine.Logic.Animations
{

    public class EndOfAnimatonSet : StateMachineBehaviour
    {

        [SerializeField] private int nextValue = 0;
        [SerializeField] private string parameterName;

        private float timestamp = 0;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timestamp = Time.time;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Time.time - timestamp >= stateInfo.length / 2)
            {
                animator.SetInteger(parameterName, nextValue);
            }
        }

    }

}
