using System;
using UnityEngine;

namespace Engine.Logic
{

    public class SkillGroupBehaviour : MonoBehaviour
    {
        
#if UNITY_EDITOR

        [SerializeField] private Vector2Int sizeGroup = new Vector2Int(3, 3);
        
        private void Start()
        {
            Destroy(this);
        }

        private Vector2 CELLS_OFFSET = new Vector2(2, 2);
        private RectTransform Rect => (RectTransform) transform;
        private Vector2 Size => new Vector2(32.5f, 32.5f);
        
        private void OnDrawGizmosSelected()
        {
            Vector2 size = Size;
            var pos = Rect.localPosition;
            var hCount = (pos.x / size.x);
            var vCount = (pos.y / size.y);
            
            pos.x = pos.x - (pos.x % size.x) + CELLS_OFFSET.x * hCount;
            pos.y = pos.y - (pos.y % size.y) + CELLS_OFFSET.y * vCount;
            Rect.localPosition = pos;
            Rect.sizeDelta = new Vector2(sizeGroup.x * Size.x, sizeGroup.y * Size.y);
        }

        private void OnValidate()
        {
            OnDrawGizmosSelected();
        }

#endif
        
    }
    
}