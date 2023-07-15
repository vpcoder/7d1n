using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcWaitStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, bool needResetAnotherActions = true, float waitTime = 1f)
        {
            this.npc = npc;

            if (needResetAnotherActions)
                npc.StopNPC();
            
            npc.CharacterContext.Actions.Add(new NpcWaitActionContext()
            {
                Action = NpcActionType.Wait,
                WaitDelay = waitTime,
            });
            npc.StartNPC();

            Destruct();
        }

        public void Destruct()
        {
            Destroy(this);
        }

    }
    
}