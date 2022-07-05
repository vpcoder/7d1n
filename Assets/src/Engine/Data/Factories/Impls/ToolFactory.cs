using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{
    
    public class ToolFactory
    {
        
        private IDictionary<string, Sprite> spriteByID = new Dictionary<string, Sprite>();

        #region Singleton

        private static readonly Lazy<ToolFactory> instance = new Lazy<ToolFactory>(() => new ToolFactory());
        public static ToolFactory Instance { get { return instance.Value; } }

        #endregion

        #region Ctor

        private ToolFactory()
        {

        }

        #endregion

        public Sprite Get(ToolType toolType)
        {
            Sprite sprite = null;
            if(!spriteByID.TryGetValue(toolType.ToString(), out sprite))
            {
                sprite = Resources.Load<Sprite>("Tools/" + toolType);
                if(sprite == null)
                {
                    Debug.LogError("sprite '" + toolType + "' not founded!");
                }
                spriteByID.Add(toolType.ToString(), sprite);
            }
            return sprite;
        }

        public string GetName(ToolType toolType)
        {
            var key = "tools/" + toolType.ToString().ToLower();
            return Localization.Instance.Get(key);
        }
    }
    
}