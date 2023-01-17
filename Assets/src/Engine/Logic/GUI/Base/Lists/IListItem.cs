using UnityEngine;

namespace Engine.Logic.Base
{
    public interface IListItem<M>
    {
	
        RectTransform Rect { get; }

        M Model { get; }

        void Construct(M model);

        void Destruct();
		
    }
    
}