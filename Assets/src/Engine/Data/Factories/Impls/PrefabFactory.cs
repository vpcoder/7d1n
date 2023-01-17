using System;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    public class PrefabFactory : PrefabFactory<GameObject>
    {
        private static readonly Lazy<PrefabFactory> instance = new Lazy<PrefabFactory>(() => new PrefabFactory());

        private PrefabFactory()
        { }

        public static PrefabFactory Instance => instance.Value;

        public override string Directory => "Background";
        
    }
    
}