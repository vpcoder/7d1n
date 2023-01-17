using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Base
{

    public interface IListWidget<T, M> : IWidget where T : class, IListItem<M>
    {

        Vector2 ItemsCellSpacing { get; }

        RectTransform Content { get; }

        List<T> Items { get; set; }

        ICollection<M> CreateModels();

        List<T> CreateContent(ICollection<M> models);

        Vector2 GetContentSize(int count);

        void ConstructUI();
		
        void DestructUI();
		
    }
    
}