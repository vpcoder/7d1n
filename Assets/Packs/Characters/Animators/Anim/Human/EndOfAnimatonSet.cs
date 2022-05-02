using UnityEngine;

namespace Engine.Logic.Animations
{

    /// <summary>
    /// 
    /// ¬ыполн€ет установку Integer параметра с именем parameterName значением nextValue, при переходе в текущее состо€ние
    /// ---
    /// Sets the Integer parameter with parameterName to the value nextValue, when passing to the current state
    /// 
    /// </summary>
    public class EndOfAnimatonSet : StateMachineBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     «начение, которое установитс€ в параметр parameterName
        ///     ---
        ///     The value that will be set in the parameterName parameter
        /// </summary>
        [SerializeField] private int nextValue = 0;

        /// <summary>
        ///     »м€ параметра, у которого необходимо помен€ть значение
        ///     ---
        ///     The name of the parameter for which you want to change the value
        /// </summary>
        [SerializeField] private string parameterName;

        #endregion

        #region Unity Events

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // ћен€ем значение параметра
            animator.SetInteger(parameterName, nextValue);
        }

        #endregion

    }

}
