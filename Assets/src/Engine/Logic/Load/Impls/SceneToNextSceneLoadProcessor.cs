using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Load
{

    public enum LoadBackgroundType
    {
        Map,
        Build,
        OpenSpace,
    };

    public class SceneToNextSceneLoadProcessor : SceneLoadProcessorBase
    {
        [SerializeField] private Image background;

        [SerializeField] private Sprite mapLoadBackground;
        [SerializeField] private Sprite buildLoadBackground;
        [SerializeField] private Sprite openSpaceLoadBackground;

        private Sprite GetSpriteByType(LoadBackgroundType type)
        {
            switch(type)
            {
                case LoadBackgroundType.Map: return mapLoadBackground;
                case LoadBackgroundType.Build: return buildLoadBackground;
                case LoadBackgroundType.OpenSpace: return openSpaceLoadBackground;
                default:
                    throw new NotSupportedException();
            }
        }

        public void ShowLoad(LoadBackgroundType type)
        {
            background.sprite = GetSpriteByType(type);
            StartCoroutine(LoadProcess());
        }

        public override void OnCompleteLoad()
        {
            
        }

        public override void OnStartLoad()
        {
            
        }

    }

}
