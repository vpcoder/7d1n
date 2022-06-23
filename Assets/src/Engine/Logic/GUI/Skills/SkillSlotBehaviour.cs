using System;
using UnityEngine;

namespace Engine.Logic
{

    public class SkillSlotBehaviour : MonoBehaviour
    {
        
#if UNITY_EDITOR

        private void Start()
        {
            Destroy(this);
        }

        private Vector2 CELLS_OFFSET = new Vector2(4, 4);
        private RectTransform Rect => (RectTransform) transform;
        private Vector2 Size => Rect.sizeDelta;
        
        private void OnDrawGizmosSelected()
        {
            Vector2 size = Size;
            var pos = Rect.localPosition;
            var hCount = (pos.x / size.x);
            var vCount = (pos.y / size.y);
            
            pos.x = pos.x - (pos.x % size.x) + CELLS_OFFSET.x * hCount;
            pos.y = pos.y - (pos.y % size.y) + CELLS_OFFSET.y * vCount;
            Rect.localPosition = pos;
        }

        private void OnValidate()
        {
            OnDrawGizmosSelected();
        }

#endif
        
    }
    
}