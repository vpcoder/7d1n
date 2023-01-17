using UnityEngine;

namespace Engine.Logic.Base.Impls
{
    
    public abstract class VerticalListWidget<T, M> : ListWidget<T, M> where T : MonoBehaviour, IListItem<M>
    {
        
        protected override void PutToLayout(T item, int index, RectTransform container)
        {
            var posY = index * (PrefabRect.sizeDelta.y + ItemsCellSpacing.y);
            var itemRect = (RectTransform)item.transform;
            var pos = itemRect.localPosition;
            pos.y = -posY + container.sizeDelta.y * 0.5f;
            itemRect.localPosition = pos;
        }

        public override Vector2 GetContentSize(int count)
        {
            var boxSize = Content.sizeDelta;
            boxSize.y = count * (PrefabRect.sizeDelta.y + ItemsCellSpacing.y);
            return boxSize;
        }
        
    }
    
}