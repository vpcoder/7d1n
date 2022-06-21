using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика эффектов метательного оружия
    /// </summary>
    public class EdgedEffectFactory : PrefabFactory<GameObject>
    {

        private static readonly Lazy<EdgedEffectFactory> instance = new Lazy<EdgedEffectFactory>(() => new EdgedEffectFactory());
        public static EdgedEffectFactory Instance { get { return instance.Value; } }
        private EdgedEffectFactory() { }

        public override string Directory => "Data/Effects/Edgeds";

    }

}
