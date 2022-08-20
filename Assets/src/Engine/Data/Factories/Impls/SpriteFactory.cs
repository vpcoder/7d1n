using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class SpriteFactory
    {

        private IDictionary<long, Sprite> spriteByID = new Dictionary<long, Sprite>();

        #region Singleton

        private static readonly Lazy<SpriteFactory> instance = new Lazy<SpriteFactory>(() => new SpriteFactory());
        public static SpriteFactory Instance { get { return instance.Value; } }

        #endregion

        #region Ctor

        private SpriteFactory()
        {

        }

        #endregion

        public Sprite Get(long id)
        {
            Sprite sprite = null;
            if(!spriteByID.TryGetValue(id, out sprite))
            {
                sprite = Resources.Load<Sprite>("Data/Sprites/" + id);
                if(sprite == null)
                {
                    Debug.LogError("sprite '" + id + "' not founded!");
                }
                spriteByID.Add(id, sprite);
            }
            return sprite;
        }

    }

}
