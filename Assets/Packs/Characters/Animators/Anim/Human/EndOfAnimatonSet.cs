using UnityEngine;

namespace Engine.Logic.Animations
{

    /// <summary>
    /// 
    /// ��������� ��������� Integer ��������� � ������ parameterName ��������� nextValue, ��� �������� � ������� ���������
    /// ---
    /// Sets the Integer parameter with parameterName to the value nextValue, when passing to the current state
    /// 
    /// </summary>
    public class EndOfAnimatonSet : StateMachineBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     ��������, ������� ����������� � �������� parameterName
        ///     ---
        ///     The value that will be set in the parameterName parameter
        /// </summary>
        [SerializeField] private int nextValue = 0;

        /// <summary>
        ///     ��� ���������, � �������� ���������� �������� ��������
        ///     ---
        ///     The name of the parameter for which you want to change the value
        /// </summary>
        [SerializeField] private string parameterName;

        #endregion

        #region Unity Events

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // ������ �������� ���������
            animator.SetInteger(parameterName, nextValue);
        }

        #endregion

    }

}
