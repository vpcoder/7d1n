using System;


namespace Engine.EGUI {

	/// <summary>
	/// Контейнер-обёртка для иконки для IconViewer
	/// </summary>
	/// <typeparam name="T">Тип хранимого объекта иконки</typeparam>
	public class IconContainer<T> : IIconContainer<T> where T : class {

		#region Hidden Fields

		/// <summary>
		/// Хранит ссылку на данные иконки
		/// </summary>
		private T item;

		#endregion

		/// <summary>
		/// Конструктор обёртки
		/// </summary>
		/// <param name="item">Устанавливает данные объекта-иконки</param>
		public IconContainer(T item) : base() {
			this.item = item;
		}

		#region Properties

		/// <summary>
		/// Устанавливает/Возвращает положение иконки в матрице по X
		/// </summary>
		public int PosX  {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает/Возвращает положение иконки в матрице по Y
		/// </summary>
		public int PosY  {
			get;
			set;
		}

		/// <summary>
		/// Устанавлвиает/Возвращает счёчик числа копий иконки
		/// </summary>
		public int Count {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает/Возвращает данные объекта-иконки
		/// </summary>
		public T Item {
			get {
				return this.item;
			}
			set {
				this.item = value;
			}
		}
		
		#endregion

	}

}
