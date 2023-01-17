using UnityEngine;

namespace Engine.Logic.Base
{
    public abstract class CellListWidget<T, M> : ListWidget<T, M>, ICellListWidget<T,M> where T : MonoBehaviour, IListItem<M>
    {

        [SerializeField] private Vector2 cellInlineOffset;
        [SerializeField] private Vector2 cellSize;
        
        public Vector2 CellInlineOffset => cellInlineOffset;
        public Vector2 CellSize => cellSize;

        public Vector2Int ViewCellsCount
        {
            get
            {
                var viewPort = Content.sizeDelta;
                return new Vector2Int {
                    x = Mathf.RoundToInt(viewPort.x / (CellSize.x + CellInlineOffset.x)),
                    y = Mathf.RoundToInt(viewPort.y / (CellSize.y + CellInlineOffset.y)),
                };
            }
        }

        private Vector2Int GetIndex(int flatIndex)
        {
            var count = ViewCellsCount;
            return new Vector2Int(){
                x = flatIndex % count.x,
                y = flatIndex / count.x,
            };
        }
		
    }
}