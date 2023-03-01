using Engine.Data.Factories.Xml;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class NpcFactory : FactoryBase<INpcInfo, XmlFactoryLoaderNpc>
    {

        private IDictionary<long, GameObject> behaviourByID = new Dictionary<long, GameObject>();
        private IDictionary<long, Sprite>     spriteByID    = new Dictionary<long, Sprite>();

        #region Singleton

        private static readonly Lazy<NpcFactory> instance = new Lazy<NpcFactory>(() => new NpcFactory());
        public static NpcFactory Instance { get { return instance.Value; } }
        private NpcFactory() { }

        #endregion

        public GameObject GetBehaviour(long id)
        {
            GameObject characterBody = null;
            if (!behaviourByID.TryGetValue(id, out characterBody))
            {
                var enemy = Get(id);
                var path = "Data/Bodies/" + enemy.BodyName;
                characterBody = Resources.Load<GameObject>(path);
#if UNITY_EDITOR
                if (characterBody == null)
                {
                    Debug.LogError("character body '" + path + "' not founded!");
                    return null;
                }
#endif
                behaviourByID.Add(id, characterBody);
            }
            return characterBody;
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
