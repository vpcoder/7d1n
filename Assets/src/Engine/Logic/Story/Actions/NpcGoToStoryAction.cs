using System.Collections.Generic;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcGoToStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private Vector3 endPos;
        private float timestamp;
        private bool isStart;

        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, Vector3 targetPos, bool needResetAnotherActions = true, MoveSpeedType moveSpeedType = MoveSpeedType.Run, float speed = 1f)
        {
            this.npc = npc;
            this.endPos = targetPos;
            timestamp = Time.time;

            var path = npc.CalculatePath(endPos);
            if (Lists.IsEmpty(path))
            {
                // Как такое произошло??
                // WTF??
                path = new List<Vector3> { endPos };
            }
            
            if(needResetAnotherActions)
                npc.CharacterContext.Actions.Clear();
            
            npc.CharacterContext.Actions.Add(new NpcMoveActionContext()
            {
                Action = NpcActionType.Move,
                Path = path,
                Speed = speed,
                MoveSpeedType = moveSpeedType,
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