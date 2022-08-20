using System;
using UnityEngine;

namespace Engine.EGUI
{
    
    public enum ItemsOrientation {
        Horizontal,
        Vertical,
    }
    
    public abstract class ListView<T, M> : View<T, M> where T : MonoBehaviour
                                                      where M : class
    {

        [SerializeField] private ItemsOrientation orientation = ItemsOrientation.Vertical;
        [SerializeField] private Vector2 insideOffset = Vector2.zero;

        private Rect ItemSize => ((RectTransform)PrefabItem.transform).rect;
        
        public override Vector2 GetContentSize()
        {
            var size = new Vector2();
            var count = Models.Count;
            var itemSize = ItemSize;
            switch (orientation)
            {
                case ItemsOrientation.Vertical:
                    var height = itemSize.height;
                    size.x = Content.sizeDelta.x;
                    size.y = (height + insideOffset.y * 2) * count;
                    break;
                case ItemsOrientation.Horizontal:
                    var width = itemSize.width;
                    size.x = (width + insideOffset.x * 2) * count;
                    size.y = Content.sizeDelta.y;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return size;
        }

        public override Vector3 GetItemPosition(int index)
        {
            var pos = new Vector3();
            var itemSize = ItemSize;
            switch (orientation)
            {
                case ItemsOrientation.Vertical:
                    pos.x = insideOffset.x;
                    pos.y = -insideOffset.y - (itemSize.height + insideOffset.y) * index;
                    break;
                case ItemsOrientation.Horizontal:
                    pos.x = insideOffset.x + (itemSize.width + insideOffset.x) * index;
                    pos.y = insideOffset.y;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return pos;
        }
        
    }
    
}