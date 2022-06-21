using System;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика эффектов снарядов
    /// </summary>
    public class BulletEffectFactory : PrefabFactory<GameObject>
    {

        private static readonly Lazy<BulletEffectFactory> instance = new Lazy<BulletEffectFactory>(() => new BulletEffectFactory());
        public static BulletEffectFactory Instance { get { return instance.Value; } }
        private BulletEffectFactory() { }

        public override string Directory => "Data/Effects/Bullets";

    }

}
