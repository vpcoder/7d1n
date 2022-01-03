using System;


namespace Engine.EGUI {

	/// <summary>
	/// Универсальный интерфейс обёртки иконки для IconViewer
	/// </summary>
	/// <typeparam name="T">Тип данных иконки</typeparam>
	public interface IIconContainer<T> where T : class {

		/// <summary>
		/// Устанавливает/Возвращает положение иконки в матрице по X
		/// </summary>
		int PosX { get; set; }

		/// <summary>
		/// Устанавливает/Возвращает положение иконки в матрице по Y
		/// </summary>
		int PosY { get; set; }

		/// <summary>
		/// Устанавливает/Возвращает ссылку на данные иконки
		/// </summary>
		T Item { get; set; }

		/// <summary>
		/// Количество предметов в группе
		/// </summary>
		int Count { get; set;  }

	}

}
