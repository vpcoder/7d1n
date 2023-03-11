using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class LookAtStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private Quaternion startRotation;
        private Quaternion endRotation;
        private float timestamp;
        private float speed;
        private bool isStart;

        private Transform source;
        
        public void Init(Transform source, Transform target, float speed)
        {
            this.source = source;
            this.speed = speed;
            startRotation = source.rotation;
            source.LookAt(target);
            endRotation = source.rotation;
            source.rotation = startRotation;
            timestamp = Time.time;
            isStart = true;
        }

        public void Destruct()
        {
            source.rotation = endRotation;
            
            isStart = false;
            Destroy(this);
        }

        private void Update()
        {
            if(!isStart)
                return;
            
            var progress = Mathf.Min(1f, (Time.time - timestamp) * speed);
            source.rotation = Quaternion.Lerp(startRotation, endRotation, progress);

            if (progress >= 1f)
                Destruct();
        }
        
    }
    
}