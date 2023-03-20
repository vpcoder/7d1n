using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Итератор действия перемещения.
    /// Итератор выполняет перемещение NPC по указанной траектории с указанной скоростью, при этом кстанавливается указанная анимация перемещения
    /// ---
    /// The iterator of the move action.
    /// The iterator moves the NPC along the specified trajectory at the specified speed, and the specified movement animation stops
    /// 
    /// </summary>
    public class MoveAiIteration : AiIterationActionBase<NpcMoveActionContext>
    {

        public override NpcActionType ActionType => NpcActionType.Move;

        public override bool Iteration(CharacterNpcBehaviour npc, NpcMoveActionContext actionContext, float timestamp)
        {

            npc.Animator.SetInteger(AnimationKey.WeaponEquipKey, (int)MoveSpeedType.Run);

            var nextPoint = actionContext.Path[0];
            var nextRotation = GetLookAtRotation(npc, nextPoint);

            var distance = Vector3.Distance(actionContext.StartPosition, nextPoint);

            if (npc.Target != null && Vector3.Distance(npc.Target.ToObject.transform.position, npc.transform.position) <= 1f && actionContext.Path.Count == 1) // Подошли слишком близко к последней точке
            {
                return true; // Конец этого действия
            }

            var rotationProgress = Mathf.Min((Time.time - actionContext.Timestamp) * 1.7f * actionContext.Speed, 1f);
            npc.transform.rotation = Quaternion.Lerp(actionContext.StartRotation, nextRotation, rotationProgress);

            var positionProgress = Mathf.Min((Time.time - actionContext.Timestamp) * actionContext.Speed / distance, 1f);
            npc.transform.position = Vector3.Lerp(actionContext.StartPosition, nextPoint, positionProgress);
            if (positionProgress >= 1f)
            {
                actionContext.Path.RemoveAt(0); // Следующая точка
                actionContext.StartPosition = npc.transform.position;
                actionContext.StartRotation = npc.transform.rotation;
                actionContext.Timestamp = Time.time;
                if (actionContext.Path.Count == 0) // Достигли конца
                {
                    return true; // Конец этого действия
                }
            }

            return false;
        }

        private Quaternion GetLookAtRotation(CharacterNpcBehaviour npc, Vector3 nextPoint)
        {
            npc.LookDirectionTransform.LookAt(nextPoint);
            var nextRotate = npc.LookDirectionTransform.rotation;
            nextRotate.x = 0;
            nextRotate.z = 0;
            return nextRotate;
        }

        public override void Start(CharacterNpcBehaviour npc, NpcMoveActionContext actionContext)
        {
            actionContext.StartPosition = npc.transform.position;
            actionContext.StartRotation = npc.transform.rotation;
            actionContext.Timestamp     = Time.time;
        }

        public override void End(CharacterNpcBehaviour npc, NpcMoveActionContext actionContext, float timestamp)
        { }

    }

}
