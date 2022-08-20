using Engine.Logic.Locations;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Animations
{

    /// <summary>
    /// 
    /// ��������� "��������" NPC.
    /// �� ���������� �������� ��������� �������� ��������� ����������� �������� ��, ������ ����� � �.�.
    /// ---
    /// Performs "killing" of NPCs.
    /// When the current state of the animation is complete, performs destruction of AI scripts, pathfinding, etc.
    /// 
    /// </summary>
    public class NpcDeadAnimationSet : StateMachineBehaviour
    {

        #region Unity Events

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Dead(animator);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     ��������� ����������� ��������, ����������� ������ ��
        ///     ---
        ///     Performs destruction of scripts that perform AI work
        /// </summary>
        /// <param name="animator">
        ///     ��������� ��������� �� ������� ����������� ������� ������, NPC �������� ����� �����
        ///     ---
        ///     The animator instance on which the current script is running, which NPC you want to kill
        /// </param>
        private void Dead(Animator animator)
        {
            GameObject.Destroy(animator.gameObject.GetComponent<NpcDamagedBehaviour>());
            GameObject.Destroy(animator.gameObject.GetComponent<EnemyNpcBehaviour>());
            GameObject.Destroy(animator.gameObject.GetComponent<NavMeshAgent>());
            GameObject.Destroy(animator.gameObject.GetComponent<Collider>());
            GameObject.Destroy(animator);
        }

        #endregion

    }

}
