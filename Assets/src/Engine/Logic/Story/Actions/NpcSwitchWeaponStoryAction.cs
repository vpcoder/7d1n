using System.Linq;
using Engine.Data;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcSwitchWeaponStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private Quaternion endRot;
        private bool isStart;

        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, long weaponID)
        {
            var weapon = npc.CharacterBody.Character?.Weapons?
                .FirstOrDefault(o => o.ID == weaponID);
            Init(npc, weapon);
        }
        
        public void Init(CharacterNpcBehaviour npc, IWeapon weapon)
        {
            this.npc = npc;
            
            npc.CharacterContext.Actions.Clear();
            npc.CharacterContext.Actions.Add(new NpcPickWeaponActionContext()
            {
                Action = NpcActionType.PickWeapon,
                Weapon = weapon,
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