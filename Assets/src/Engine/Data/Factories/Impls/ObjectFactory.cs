using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class ObjectFactory
    {

        private IDictionary<long, GameObject> prefabByID = new Dictionary<long, GameObject>();

        #region Singleton

        private static readonly Lazy<ObjectFactory> instance = new Lazy<ObjectFactory>(() => new ObjectFactory());
        public static ObjectFactory Instance { get { return instance.Value; } }
        private ObjectFactory() { }

        #endregion

        public GameObject Get(long id)
        {
            GameObject prefab = null;
            if(!prefabByID.TryGetValue(id, out prefab))
            {
                prefab = Resources.Load<GameObject>("Data/Objects/" + id);
                if(prefab == null)
                {
                    Debug.LogError("object '" + id + "' not founded!");
                }
                prefabByID.Add(id, prefab);
            }
            return prefab;
        }

    }

}
