using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.EGUI
{
    
    public abstract class View<T, M> : Panel, IView<T, M> where T : MonoBehaviour
                                                          where M : class
    {

        [SerializeField] private RectTransform content;
        [SerializeField] private T prefab;

        private readonly IList<M> models = new List<M>();
        private readonly IList<T> items = new List<T>();

        public RectTransform Content => content;
        public T PrefabItem => prefab;
        public ICollection<M> Models => models;
        public ICollection<T> Items => items;
        
        public abstract void InitItem(M model, T item, int index);
        public abstract void DisposeItem(T item);
        public abstract ICollection<M> ProvideModels();
        public abstract Vector2 GetContentSize();
        public abstract Vector3 GetItemPosition(int index);

        public override void Show()
        {
            base.Show();
            Redraw();
        }
        
        public override void Hide()
        {
            Clean();
            base.Hide();
        }
        
        public void Redraw()
        {
            Clean();
            models.AddRange(ProvideModels());
            if(Lists.IsEmpty(Models)) 
                return;

            var index = 0;
            Content.sizeDelta = GetContentSize();
            foreach (var model in Models)
            {
                var item = Instantiate(PrefabItem, Vector3.zero, Quaternion.identity, Content);
                item.transform.localPosition = GetItemPosition(index++);
                InitItem(model, item, index);
                Items.Add(item);
            }
        }

        private void Clean()
        {
            models.Clear();
            foreach (var item in items)
            {
                DisposeItem(item);
                GameObject.Destroy(item.gameObject);
            }
            items.Clear();
        }

    }
    
}