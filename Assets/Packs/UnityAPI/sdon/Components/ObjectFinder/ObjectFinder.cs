using Engine.Logic.Dialog;

namespace UnityEngine
{

    public class ObjectFinder
    {

        public static Transform Get(string tag)
        {
            return GameObject.FindWithTag(tag).transform;
        }

        public static T Get<T>(string tag)
        {
            return GameObject.FindWithTag(tag).GetComponent<T>();
        }

        public static T Find<T>() where T : Behaviour
        {
            return GameObject.FindObjectOfType<T>();
        }

        public static Canvas Canvas => Get<Canvas>("Canvas");
        public static DialogBox DialogBox => Get<DialogBox>("DialogBox");
        public static GameObject TopPanel => GameObject.FindWithTag("TopPanel");
        
        public static void Clear()
        {
            
        }

    }

}
