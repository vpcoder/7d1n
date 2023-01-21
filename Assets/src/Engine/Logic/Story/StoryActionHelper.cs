using Engine.Story.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Story
{
    
    public static class StoryActionHelper
    {

        private static T Create<T>(GameObject source) where T : MonoBehaviour
        {
            var prev = source.GetComponent<T>();
            if (prev != null)
                prev.Destroy();
            
            return source.AddComponent<T>();
        }
        
        public static void LookAt(Transform source, Transform target, float speed = 1f)
        {
            Create<LookAtStoryAction>(source.gameObject).Init(source.transform, target, speed);
        }
        
        public static void LookAt(Camera source, Transform target, float speed = 1f)
        {
            LookAt(source.transform, target, speed);
        }
        
        public static void LookAt(GameObject source, Transform target, float speed = 1f)
        {
            LookAt(source.transform, target, speed);
        }

        public static void Fade(Image image, Color from, Color to, float speed = 1f)
        {
            Create<BackgroundFaderStoryAction>(image.gameObject).Init(image, from, to, speed);
        }
        
    }
    
}