using Engine.Data;
using System.Collections.Generic;

namespace Engine.Logic
{

    /// <summary>
    /// Интерфейс сумки
    /// </summary>
	public interface IBag
    {
		
        /// <summary>
        /// Список предметов в сумке
        /// </summary>
		IList<IItem> Items { get; set; }

        /// <summary>
        /// Ширина всей видимой области инвентаря
        /// </summary>
        int FrameWidth { get; }

        /// <summary>
        /// Высота всей видимой области инвентаря
        /// </summary>
        int FrameHeight { get; }

        /// <summary>
        /// Количество ячеек умещающихся в ширину инвентаря
        /// </summary>
        int CellCountX { get; }

        /// <summary>
        ///  Количество ячеек умещающихся в высоту инвентаря
        /// </summary>
        int CellCountY { get; }

        /// <summary>
        /// Перерисовка сумки
        /// </summary>
        void Redraw();

        /// <summary>
        /// Клик по предмету в этой сумке
        /// </summary>
        /// <param name="item">предмет, по которому кликнули</param>
        void ClickItem(IItem item);

        /// <summary>
        /// Выбор определённого предмета в сумке
        /// </summary>
        /// <param name="item">Предмет, который выбрали</param>
        void SetSelected(AbstractItem item);

    }
	
}
