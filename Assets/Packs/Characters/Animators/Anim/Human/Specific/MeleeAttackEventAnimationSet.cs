using Engine.Logic.Locations;
using Engine.Logic.Locations.Battle.Actions;
using UnityEngine;

namespace Engine.Logic.Animations
{

    /// <summary>
    /// 
    /// В конце текущей анимации будет произведеног действие передачи урона
    /// Момент когда необходимо искать того по кому попали и передавать урон
    /// ---
    /// At the end of the current animation the damage transfer action will be done
    /// The moment when it is necessary to look for the one who was hit and transfer the damage
    /// 
    /// </summary>
    public class MeleeAttackEventAnimationSet : StateMachineBehaviour
    {

        #region Unity Events

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var enemy = animator.gameObject.GetComponent<EnemyNpcBehaviour>();
            if (enemy == null)
                return;
            
            var context = ObjectFinder.Find<BattleActionsController>().AttackContext;
            CharacterBattleActionFactory.Instance.InvokeProcess(CharacterBattleAction.EndMeleeAttack, context);
        }

        #endregion

    }

}
