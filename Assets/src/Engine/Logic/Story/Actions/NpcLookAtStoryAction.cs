using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcLookAtStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private Quaternion endRot;
        private float timestamp;
        private bool isStart;

        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, Transform target, float speed = 1f)
        {
            this.npc = npc;
            timestamp = Time.time;

            var direction = npc.LookDirectionTransform.rotation;
            npc.LookDirectionTransform.LookAt(target);
            this.endRot = npc.LookDirectionTransform.rotation;
            npc.LookDirectionTransform.rotation = direction;
            
            npc.CharacterContext.Actions.Clear();
            npc.CharacterContext.Actions.Add(new NpcLookActionContext()
            {
                Action = NpcActionType.Look,
                LookPoint = target.position,
                Speed = speed,
            });
            npc.StartNPC();
            
            isStart = true;
        }

        public void Destruct()
        {
            npc.transform.rotation = endRot;
            
            isStart = false;
            Destroy(this);
        }

        private void Update()
        {
            if(!isStart)
                return;
            
            // Все действия завершены?
            // All actions completed?
            if (Lists.IsEmpty(npc.CharacterContext.Actions)
                // Таймаут на совершение действия
                // Timeout for the action
                || Time.time - timestamp > 10f)
                Destruct();
        }
        
    }
    
}