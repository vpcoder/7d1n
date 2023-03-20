using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Итератор действия обзора вокруг.
    /// Итератор выполняет поворот NPC в точку интереса из контекста операции
    /// ---
    /// The iterator of the look-around action.
    /// The iterator performs the rotation of the NPC to the point of interest from the context of the operation
    /// 
    /// </summary>
    public class LookAiIteration : AiIterationActionBase<NpcLookActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.Look;

        public override bool Iteration(CharacterNpcBehaviour npc, NpcLookActionContext actionContext, float timestamp)
        {
            var nextRotation = GetLookAtRotation(npc, actionContext.LookPoint);
            var rotationProgress = Mathf.Min((Time.time - timestamp) * 1.7f * actionContext.Speed, 1f);
            npc.transform.rotation = Quaternion.Lerp(actionContext.StartRotation, nextRotation, rotationProgress);
            return rotationProgress >= 1f;
        }

        private Quaternion GetLookAtRotation(CharacterNpcBehaviour npc, Vector3 nextPoint)
        {
            npc.LookDirectionTransform.LookAt(nextPoint);
            var nextRotate = npc.LookDirectionTransform.rotation;
            nextRotate.x = 0;
            nextRotate.z = 0;
            return nextRotate;
        }

        public override void Start(CharacterNpcBehaviour npc, NpcLookActionContext actionContext)
        {
            actionContext.StartRotation = npc.transform.rotation;
        }

        public override void End(CharacterNpcBehaviour npc, NpcLookActionContext actionContext, float timestamp)
        { }

    }

}
