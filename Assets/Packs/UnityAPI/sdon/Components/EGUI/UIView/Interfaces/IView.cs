using System.Collections.Generic;
using UnityEngine;

namespace Engine.EGUI
{
    
    public interface IView : IPanel
    {

        RectTransform Content { get; }

        void Redraw();

    }

    public interface IView<T, M> : IView where T : MonoBehaviour
                                         where M : class
    {

        ICollection<M> Models { get; }
        ICollection<T> Items { get; }

        T PrefabItem { get; }

        void InitItem(M model, T item, int index);

        void DisposeItem(T item);
        
        ICollection<M> ProvideModels();
        
        Vector2 GetContentSize();

    }

}