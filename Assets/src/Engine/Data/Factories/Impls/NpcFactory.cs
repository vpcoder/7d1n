using Engine.Data.Factories.Xml;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class NpcFactory : FactoryBase<INpcInfo, XmlFactoryLoaderNpc>
    {

        private IDictionary<long, GameObject> behaviourByID = new Dictionary<long, GameObject>();
        private IDictionary<long, string>     spriteByID    = new Dictionary<long, string>();

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
                var character = Get(id);
                var path = "Data/Bodies/" + character.BodyName;
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

        public string GetSprite(long id)
        {
            if (!spriteByID.TryGetValue(id, out var spriteID))
            {
                var character = Get(id);
#if UNITY_EDITOR
                if (character == null)
                {
                    Debug.LogError("sprite for npc '" + id + "' not founded!");
                    return null;
                }
#endif
                spriteByID.Add(id, character.SpriteName);
                spriteID = character.SpriteName;
            }
            return spriteID;
        }

    }

}
