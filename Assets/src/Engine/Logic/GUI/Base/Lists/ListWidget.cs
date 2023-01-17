using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Base
{
    public abstract class ListWidget<T,M> : Widget, IListWidget<T,M>  where T : MonoBehaviour, IListItem<M>
    {

        [SerializeField]
        private RectTransform content;
		
        [SerializeField]
        private T itemPrefab;

        [SerializeField] private Vector2 itemsCellSpacing = new Vector2(0f, 1f);

        public Vector2 ItemsCellSpacing => itemsCellSpacing;
        
        protected T Prefab => itemPrefab;
        protected RectTransform PrefabRect => (RectTransform)Prefab.transform;
        
        public RectTransform Content => content;

        public abstract Vector2 GetContentSize(int count);
	
        public List<T> Items { get; set; }

        public override void Show()
        {
            base.Show();
            ConstructUI();
        }

        public override void Hide()
        {
            DestructUI();
            base.Hide();
        }
        
        public List<T> CreateContent(ICollection<M> models)
        {
            var items = new List<T>();
			
            foreach(var model in models)
            {
                var item = Create(model);
                item.Construct(model);
                items.Add(item);
            }

            var size = GetContentSize(items.Count);
            Content.sizeDelta = size;

            return items;
        }

        public void ConstructUI()
        {
            var rect = Content;
            var models = CreateModels();
            var items = CreateContent(models);
            for(int i = 0; i < items.Count; i++)
                PutToLayout(items[i], i, rect);
            Items = items;
        }

        public abstract ICollection<M> CreateModels();

        protected virtual void Init(T item, M model) { }
		
        protected virtual T Create(M model)
        {
            var instance = Instantiate(itemPrefab, Content);
            Init(instance, model);
            return instance;
        }

        protected abstract void PutToLayout(T item, int index, RectTransform container);
		
        public void DestructUI()
        {
            if(Lists.IsEmpty(Items))
                return;

            foreach(var item in Items)
                item.Destruct();

            Items.Clear();
            Items = null;
        }
		
    }
}