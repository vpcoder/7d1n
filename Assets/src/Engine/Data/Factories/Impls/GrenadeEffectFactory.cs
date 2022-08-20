using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика эффектов метательного оружия
    /// </summary>
    public class GrenadeEffectFactory : PrefabFactory<GameObject>
    {

        private static readonly Lazy<GrenadeEffectFactory> instance = new Lazy<GrenadeEffectFactory>(() => new GrenadeEffectFactory());
        public static GrenadeEffectFactory Instance { get { return instance.Value; } }
        private GrenadeEffectFactory() { }

        public override string Directory => "Data/Effects/Grenades";

    }

}
