using Engine.Data.Factories.Xml;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class EnemyFactory : FactoryBase<IEnemy, XmlFactoryLoaderEnemy>
    {

        private IDictionary<long, GameObject> bodyByID = new Dictionary<long, GameObject>();

        #region Singleton

        private static readonly Lazy<EnemyFactory> instance = new Lazy<EnemyFactory>(() => new EnemyFactory());
        public static EnemyFactory Instance { get { return instance.Value; } }
        private EnemyFactory() { }

        #endregion

        public GameObject GetBody(long id)
        {
            GameObject body = null;
            if (!bodyByID.TryGetValue(id, out body))
            {
                var enemy = Get(id);
                var path = "Data/Enemies/" + enemy.SpritePath;
                body = Resources.Load<GameObject>(path);
#if UNITY_EDITOR
                if (body == null)
                {
                    Debug.LogError("body '" + path + "' not founded!");
                    return null;
                }
#endif
                bodyByID.Add(id, body);
            }
            return body;
        }

    }

}
