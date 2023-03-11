using UnityEngine;

namespace Engine.Story.Actions
{
    
    public class LookAtAndMoveStoryAction : MonoBehaviour, IActionDestruct
    {
        
        private float timestamp;
        private float speed;
        private bool isStart;

        private Transform sourceObject;
        private Transform lookAtTarget;

        private Quaternion startRot;
        private Quaternion endRot;
        private Vector3 moveToPoint;
        private Vector3 startPoint;
        
        public void Init(Transform sourceObject, Transform lookAtTarget, Vector3 moveToPoint, float speed)
        {
            this.sourceObject = sourceObject;
            this.lookAtTarget = lookAtTarget;
            this.moveToPoint = moveToPoint;
            this.startPoint = sourceObject.position;

            this.startRot = sourceObject.rotation;
            sourceObject.LookAt(lookAtTarget);
            this.endRot = sourceObject.rotation;
            sourceObject.rotation = startRot;
            
            this.speed = speed;
            timestamp = Time.time;
            isStart = true;
        }

        public void Destruct()
        {
            sourceObject.position = moveToPoint;
            sourceObject.LookAt(lookAtTarget);
            
            isStart = false;
            Destroy(this);
        }

        private void Update()
        {
            if(!isStart)
                return;
            
            var progress = Mathf.Min(1f, (Time.time - timestamp) * speed);
            sourceObject.position = Vector3.Lerp(startPoint, moveToPoint, progress);
            sourceObject.LookAt(lookAtTarget);
            
            if (progress >= 1f)
                Destruct();
        }
        
    }
    
}