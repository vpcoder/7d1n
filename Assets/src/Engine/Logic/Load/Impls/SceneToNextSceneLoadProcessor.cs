using Engine.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Load
{

    public class SceneToNextSceneLoadProcessor : SceneLoadProcessorBase
    {
        [SerializeField] private Image background;

        private Sprite GetSpriteByType(SceneName type)
        {
            return Resources.Load<Sprite>("Load/" + type.ToString());
        }

        public void ShowLoad(SceneName type)
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
