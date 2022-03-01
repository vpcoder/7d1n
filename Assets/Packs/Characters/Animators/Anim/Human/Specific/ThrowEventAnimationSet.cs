using Engine.Logic.Locations;
using Engine.Logic.Locations.Battle.Actions;
using UnityEngine;

namespace Engine.Logic.Animations
{

    /// <summary>
    /// 
    /// В конце текущей анимации будет произведеног действие броска
    /// Момент когда необходимо кидать нож/сюрикен/гранату и т.д.
    /// ---
    /// At the end of the current animation a throw action will be performed
    /// The moment when it is necessary to throw a knife/suriken/grenade, etc.
    /// 
    /// </summary>
    public class ThrowEventAnimationSet : StateMachineBehaviour
    {

        #region Unity Events

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var enemy = animator.gameObject.GetComponent<EnemyNpcBehaviour>();
            if (enemy == null)
                return;

            var context = ObjectFinder.Find<BattleActionsController>().AttackContext;
            CharacterBattleActionFactory.Instance.InvokeProcess(CharacterBattleAction.EndThrowAttack, context);
        }

        #endregion

    }

}
