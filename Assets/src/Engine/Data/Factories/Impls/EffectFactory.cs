using System;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    public class EffectFactory : PrefabFactory<GameObject>
    {
        private static readonly Lazy<EffectFactory> instance = new Lazy<EffectFactory>(() => new EffectFactory());

        private EffectFactory()
        { }

        public static EffectFactory Instance => instance.Value;

        public override string Directory => "Effects";

        public GameObject AddEffect(string id)
        {
            var prefab = Get(id);
            var effect = UnityEngine.Object.Instantiate(prefab, ObjectFinder.Canvas.transform);
            effect.transform.SetAsLastSibling();
            return effect;
        }
        
    }
    
}