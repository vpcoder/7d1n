using UnityEngine;

namespace Engine.Logic.Base
{
    
    public abstract class ListItem<M> : MonoBehaviour, IListItem<M>
    {

        public M Model { get; private set; }
        public RectTransform Rect => (RectTransform)transform;

        public virtual void Construct(M model)
        {
            Model = model;
        }

        public abstract void Destruct();
				
    }
    
}