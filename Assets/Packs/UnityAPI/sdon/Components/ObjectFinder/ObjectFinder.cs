using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine.UI;

namespace UnityEngine
{

    public class ObjectFinder
    {

        public static Transform Get(string tag)
        {
            var item = GameObject.FindWithTag(tag);
            return item == null ? null : item.transform;
        }

        public static T Get<T>(string tag) where T : class
        {
            var item = GameObject.FindWithTag(tag).GetComponent<T>();
            return (item == null) ? null : item;
        }

        public static T Find<T>() where T : Behaviour
        {
            var item = Object.FindObjectOfType<T>();
            return (item == null) ? null : item;
        }

        public static LocationCharacter Character => Find<LocationCharacter>();
        public static Canvas Canvas => Get<Canvas>("Canvas");
        public static DialogBox DialogBox => Get<DialogBox>("DialogBox");
        public static GameObject TopPanel => GameObject.FindWithTag("TopPanel");
        public static GameObject SceneView => GameObject.FindWithTag("SceneView");
        public static Image SceneViewImage => SceneView.GetComponent<Image>();
        
        public static void Clear()
        {
            
        }

    }

}
