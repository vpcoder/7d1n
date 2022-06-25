using Engine.Data;
using System.Collections.Generic;

namespace Engine.Logic
{

    /// <summary>
    ///
    /// Интерфейс сумки
    /// Сумка содержит предметы, общие описания необходимые для минимальной реализации графической части сумки
    /// ---
    /// Bag interface
    /// The bag contains items, general descriptions necessary for the minimal implementation of the graphical part of the bag
    /// 
    /// </summary>
	public interface IBag
    {
		
        /// <summary>
        /// 	Список предметов в сумке
        /// 	---
        /// 	List of items in the bag
        /// </summary>
		IList<IItem> Items { get; set; }

        /// <summary>
        ///		Ширина всей видимой области инвентаря
        ///		---
        ///		The width of the entire visible area of the inventory
        /// </summary>
        int FrameWidth { get; }

        /// <summary>
        ///		Высота всей видимой области инвентаря
        ///		---
        ///		The height of the entire visible area of the inventory
        /// </summary>
        int FrameHeight { get; }

        /// <summary>
        ///		Количество ячеек умещающихся в ширину инвентаря
        ///		---
        ///		Number of cells that fit into the width of the inventory
        /// </summary>
        int CellCountX { get; }

        /// <summary>
        ///		Количество ячеек умещающихся в высоту инвентаря
        ///		---
        ///		Number of cells that fit in the height of the inventory
        /// </summary>
        int CellCountY { get; }

        /// <summary>
        ///		Перерисовка сумки
        ///		Выполняет построение графических элементов для формирования UI, который можно показывать игроку
        ///		---
        /// 	Redrawing the bag
        /// 	Builds graphical elements to form a UI that can be shown to the player
        /// </summary>
        void Redraw();

        /// <summary>
        /// 	Клик по предмету в этой сумке
        /// 	Метод будет получать уведомления когда игрок кликает по предметы находящемуся В ЭТОЙ сумке
        /// 	---
        /// 	Click on an item in this bag
        /// 	The method will receive a notification when a player clicks an item in THIS bag
        /// </summary>
        /// <param name="item">
        /// 	Предмет, по которому кликнули
        /// 	---
        /// 	Item clicked on
        /// </param>
        void ClickItem(IItem item);

        /// <summary>
        /// 	Выбор определённого предмета в сумке
        /// 	Выделяет предмет, подсвечивая пользователю
        /// 	---
        /// 	Selects a specific item in the bag
        /// 	Highlighting the item, highlighting it to the user
        /// </summary>
        /// <param name="itemBehaviour">
        ///		Предмет, который выбрали
        ///		---
        ///		The item chosen
        /// </param>
        void SetSelected(InventoryItemBehaviour itemBehaviour);

        InventoryItemBehaviour Selected { get; }

    }
	
}
