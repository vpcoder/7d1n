using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Итератор действия ожидания.
    /// Итератор ждёт указанный промежуток времени, держа NPC в состоянии "Стоять на месте"
    /// ---
    /// The iterator of the wait action.
    /// The iterator waits for the specified amount of time, keeping the NPC in the "Stand Idle" state
    /// 
    /// </summary>
    public class WaitAiIteration : AiIterationActionBase<NpcWaitActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.Wait;

        public override bool Iteration(EnemyNpcBehaviour npc, NpcWaitActionContext actionContext, float timestamp)
        {
            npc.Animator.SetInteger(AnimationKey.MoveSpeedKey, (int)MoveSpeedType.Idle);

            return Time.time - timestamp >= actionContext.WaitDelay;
        }

        public override void Start(EnemyNpcBehaviour npc, NpcWaitActionContext actionContext)
        { }

        public override void End(EnemyNpcBehaviour npc, NpcWaitActionContext actionContext, float timestamp)
        { }

    }

}
