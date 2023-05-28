using System.Collections.Generic;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcGoToStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private Vector3 endPos;
        private float timestamp;
        private bool isStart;

        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, Transform target, float speed = 1f)
        {
            this.npc = npc;
            this.endPos = target.position;
            timestamp = Time.time;

            var path = npc.CalculatePath(endPos);
            if (Lists.IsEmpty(path))
            {
                // Как такое произошло??
                // WTF??
                path = new List<Vector3> { endPos };
            }
            
            npc.CharacterContext.Actions.Clear();
            npc.CharacterContext.Actions.Add(new NpcMoveActionContext()
            {
                Action = NpcActionType.Move,
                Path = path,
                Speed = speed,
            });
            npc.StartNPC();
            
            isStart = true;
        }

        public void Destruct()
        {
            npc.transform.position = endPos;
            
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