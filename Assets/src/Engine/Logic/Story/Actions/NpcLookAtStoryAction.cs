using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class NpcLookAtStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private Quaternion startRotation;
        private Quaternion endRotation;
        private float timestamp;
        private float speed;
        private bool isStart;

        private CharacterNpcBehaviour npc;
        
        public void Init(CharacterNpcBehaviour npc, Transform target, float speed)
        {
            this.npc = npc;
            this.speed = speed;
            startRotation = npc.transform.rotation;
            npc.transform.LookAt(target);
            endRotation = Quaternion.Euler(npc.transform.rotation.eulerAngles.x, 0f, 0f);
            npc.transform.rotation = startRotation;
            timestamp = Time.time;
            isStart = true;
        }

        public void Destruct()
        {
            npc.transform.rotation = endRotation;
            
            isStart = false;
            Destroy(this);
        }

        private void Update()
        {
            if(!isStart)
                return;
            
            var progress = Mathf.Min(1f, (Time.time - timestamp) * speed);
            npc.transform.rotation = Quaternion.Lerp(startRotation, endRotation, progress);

            if (progress >= 1f)
                Destruct();
        }
        
    }
    
}