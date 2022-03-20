using Engine.Data.Factories.Xml;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class NpcFactory : FactoryBase<INpcInfo, XmlFactoryLoaderNpc>
    {

        private IDictionary<long, GameObject> bodyByID      = new Dictionary<long, GameObject>();
        private IDictionary<long, GameObject> behaviourByID = new Dictionary<long, GameObject>();
        private IDictionary<long, Sprite>     spriteByID    = new Dictionary<long, Sprite>();

        #region Singleton

        private static readonly Lazy<NpcFactory> instance = new Lazy<NpcFactory>(() => new NpcFactory());
        public static NpcFactory Instance { get { return instance.Value; } }
        private NpcFactory() { }

        #endregion

        public GameObject GetBody(long id)
        {
            GameObject body = null;
            if (!bodyByID.TryGetValue(id, out body))
            {
                var enemy = Get(id);
                var path = "Data/Bodies/" + enemy.BodyName;
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

        public GameObject GetBehaviour(long id)
        {
            GameObject behaviour = null;
            if (!behaviourByID.TryGetValue(id, out behaviour))
            {
                var enemy = Get(id);
                var path = "Data/Behaviours/" + enemy.BehaviourName;
                behaviour = Resources.Load<GameObject>(path);
#if UNITY_EDITOR
                if (behaviour == null)
                {
                    Debug.LogError("behaviour '" + path + "' not founded!");
                    return null;
                }
#endif
                behaviourByID.Add(id, behaviour);
            }
            return behaviour;
        }

        public Sprite GetSprite(long id)
        {
            Sprite sprite = null;
            if (!spriteByID.TryGetValue(id, out sprite))
            {
                var enemy = Get(id);
                var path = "Data/Sprites/" + enemy.SpriteName;
                sprite = Resources.Load<Sprite>(path);
#if UNITY_EDITOR
                if (sprite == null)
                {
                    Debug.LogError("sprite '" + path + "' not founded!");
                    return null;
                }
#endif
                spriteByID.Add(id, sprite);
            }
            return sprite;
        }

    }

}
