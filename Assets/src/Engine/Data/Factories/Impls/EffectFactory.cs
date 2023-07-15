using System;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    public class EffectFactory : PrefabFactory<GameObject>
    {
        
        #region Singleton
        
        private static readonly Lazy<EffectFactory> instance = new Lazy<EffectFactory>(() => new EffectFactory());

        private EffectFactory()
        { }

        public static EffectFactory Instance => instance.Value;
        
        #endregion

        public const string DAMAGE_HINT = "damage_hint";
        public const string QUEST_HINT = "quest_hint";
        public const string TALK_HINT = "talk_hint";
        public const string MESSAGE_HINT = "message_hint";
        
        public override string Directory => "Data/Effects";

        public GameObject AddEffect(string id)
        {
            var prefab = Get(id);
            var effect = UnityEngine.Object.Instantiate(prefab, ObjectFinder.Canvas.transform);
            effect.transform.SetAsLastSibling();
            return effect;
        }
        
    }
    
}