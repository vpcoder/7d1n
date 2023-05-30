using System;
using Engine.Data.Repositories;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class AvatarFactory : PrefabFactory<Sprite>
    {

        #region Singleton
        
        private static readonly Lazy<AvatarFactory> instance = new Lazy<AvatarFactory>(() => new AvatarFactory());

        private AvatarFactory() { }

        public static AvatarFactory Instance => instance.Value;
        
        #endregion

        public override string Directory => "Avatars";
        
    }
    
}