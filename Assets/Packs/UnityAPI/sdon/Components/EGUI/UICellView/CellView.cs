using Engine.EGUI;

namespace UnityEngine.UICellView
{
    
    public abstract class CellView<T, M> : View<T, M> where T : MonoBehaviour
                                                      where M : class
    {

        #region Hidden Fields
        
        [SerializeField] private Vector2 insideOffset = Vector2.zero;
        
        #endregion
        
        #region Properties
        
        private Vector2 ItemSize => ((RectTransform)PrefabItem.transform).sizeDelta;
        
        private Vector2 ContentSize => ((RectTransform)Content.transform).sizeDelta;
        
        private int MaxRowCount => Mathf.RoundToInt(ContentSize.x / (ItemSize.x + insideOffset.x * 2f));
        
        #endregion
        
        private Vector2Int GetXY(int flatIndex)
        {
            var xCount = MaxRowCount;
            var index = new Vector2Int();
            index.x = flatIndex % xCount;
            index.y = flatIndex / xCount;
            return index;
        }
        
        public override Vector2 GetContentSize()
        {
            var size = new Vector2();
            var count = Models.Count;
            var itemSize = ItemSize;

            var lastIndex = GetXY(count);

            size.x = ContentSize.x;
            size.y = lastIndex.y * (itemSize.y + insideOffset.y);
            
            return size;
        }

        public override Vector3 GetItemPosition(int flatIndex)
        {
            var pos = new Vector3();
            var itemSize = ItemSize;
            var index = GetXY(flatIndex);
            pos.x = index.x * (itemSize.x + insideOffset.x);
            pos.y = index.y * (itemSize.y + insideOffset.y);
            return pos;
        }

    }
    
}