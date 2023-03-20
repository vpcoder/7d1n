using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class FractionFactory
    {

        private IDictionary<OrderGroup, Sprite> spriteByFraction = new Dictionary<OrderGroup, Sprite>();

        #region Singleton

        private static readonly Lazy<FractionFactory> instance = new Lazy<FractionFactory>(() => new FractionFactory());
        public static FractionFactory Instance { get { return instance.Value; } }

        #endregion

        #region Ctor

        private FractionFactory()
        {

        }

        #endregion

        private string GetFractionPrefix(OrderGroup group)
        {
            switch (group)
            {
                case OrderGroup.PlayerGroup:
                case OrderGroup.AnotherPlayerGroup:
                    return "empty";
                case OrderGroup.WildAnimalsGroup:
                case OrderGroup.ZombieGroup:
                    return null;
                case OrderGroup.DeceasedGroup: return "deceased";
                case OrderGroup.MaraudersGroup: return "marauders";
                case OrderGroup.ScythiansGroup: return "scythians";
                case OrderGroup.NewLightGroup: return "newlight";
                case OrderGroup.TechnocratsGroup: return "technocrats";
                case OrderGroup.ReconstructionistsGroup: return "reconstructionists";
                default:
                    return null;
            }
        }
        
        public Sprite Get(OrderGroup group)
        {
            if (spriteByFraction.TryGetValue(group, out var sprite))
                return sprite;
            
            string prefix = GetFractionPrefix(group);
            if (prefix == null)
            {
                spriteByFraction.Add(group, null);
                return null;
            }
            
            sprite = Resources.Load<Sprite>("Fractions/" + prefix);
            if(sprite == null)
            {
                Debug.LogError("sprite '" + prefix + "' not founded!");
            }
            
            
            spriteByFraction.Add(group, sprite);
            
            return sprite;
        }

    }

}
