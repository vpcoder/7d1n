using System;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class AvatarFactory : PrefabFactory<Sprite>
    {
        private static readonly Lazy<AvatarFactory> instance = new Lazy<AvatarFactory>(() => new AvatarFactory());

        private AvatarFactory()
        {
        }

        public static AvatarFactory Instance => instance.Value;

        public override string Directory => "Avatars";
        
    }
    
}