using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Map
{

    public class MapBiomBackgroundCell : MonoBehaviour
    {

        [SerializeField] private Image imgBackground;

        public void Init(Sprite background, Vector3 worldPos, Vector2 webCellSize)
        {
            imgBackground.sprite = background;
            transform.position = worldPos;
            transform.SetAsFirstSibling();

            var rect = (RectTransform)transform;
            rect.sizeDelta = new Vector2(webCellSize.x * 556.6f, webCellSize.y * 719.2f);
        }

    }

}
