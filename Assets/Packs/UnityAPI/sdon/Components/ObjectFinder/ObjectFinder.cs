using System;
using System.Collections.Generic;

namespace UnityEngine
{

    public class ObjectFinder
    {

        private static object locker = new object();
        private static IDictionary<Type, object> instances = new Dictionary<Type, object>();

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

        public static void Clear()
        {
            instances.Clear();
        }

    }

}
