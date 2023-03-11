using UnityEngine;
using UnityEngine.UI;

namespace Engine.Story.Actions
{
    
    public class BackgroundFaderStoryAction : MonoBehaviour, IActionDestruct
    {

        private Image image;
        private float timestamp;
        private float speed;
        private bool isStart;

        private Color startColor;
        private Color endColor;

        public void Init(Image image, Color startColor, Color endColor, float speed = 1f)
        {
            if(image == null)
                Destroy(this);

            this.image = image;
            this.startColor = startColor;
            this.endColor = endColor;
            this.speed = speed;
            this.image.color = startColor;
            this.isStart = true;
            this.timestamp = Time.time;
        }
        
        public void Destruct()
        {
            image.color = endColor;
            
            isStart = false;
            Destroy(this);
        }

        private void Update()
        {
            if(!isStart)
                return;
            
            var progress = Mathf.Min(1f, (Time.time - timestamp) * speed);
            image.color = Color.Lerp(startColor, endColor, progress);

            if (progress >= 1f)
            {
                image.color = endColor;
                Destroy(this);
            }
        }
        
    }
    
}
