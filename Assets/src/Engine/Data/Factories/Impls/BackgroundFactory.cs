using System;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    public class BackgroundFactory : PrefabFactory<Sprite>
    {
        private static readonly Lazy<BackgroundFactory> instance = new Lazy<BackgroundFactory>(() => new BackgroundFactory());

        private BackgroundFactory()
        {
        }

        public static BackgroundFactory Instance => instance.Value;

        public override string Directory => "Background";
        
    }
    
}