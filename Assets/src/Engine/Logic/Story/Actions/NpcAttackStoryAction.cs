using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcAttackStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private bool isStart;

        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, bool needResetAnotherActions = true)
        {
            this.npc = npc;
            
            if (needResetAnotherActions)
                npc.StopNPC();
            
            npc.CharacterContext.Actions.Add(new NpcAttackActionContext()
            {
                Action = NpcActionType.Attack,
                Weapon = npc.Weapon,
            });
            npc.StartNPC();
            
            isStart = true;
        }

        public void Destruct()
        {
            isStart = false;
            Destroy(this);
        }

        private void Update()
        {
            if(!isStart)
                return;
            
            // Все действия завершены?
            // All actions completed?
            if (Lists.IsEmpty(npc.CharacterContext.Actions))
                Destruct();
        }
        
    }
    
}