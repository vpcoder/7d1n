using UnityEngine;

namespace Engine.Logic.Base
{
    
    public interface ICellListWidget<T, M> : IListWidget<T, M> where T : class, IListItem<M>
    {

        Vector2 CellInlineOffset { get; }
		
        Vector2 CellSize { get; }

        Vector2Int ViewCellsCount { get; }
			
    }
    
}