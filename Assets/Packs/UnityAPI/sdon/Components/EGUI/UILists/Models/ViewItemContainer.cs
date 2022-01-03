using System;

namespace Engine.EGUI {

	/// <summary>
	/// Промежуточный класс-контейнер, который позволит передавать структуры и enum-ы внутрь провайдеров
	/// </summary>
	/// <typeparam name="T">Тип значения внутри контейнера</typeparam>
	[Serializable]
	public class ViewItemContainer<T> : IViewItemContainer<T>
		where T : struct {

		/// <summary>
		/// Свойство с данными
		/// </summary>
		public T Value {
			get;
			set;
		}

		/// <summary>
		/// Контейнер обязан быть проинициализирован в конструкторе
		/// </summary>
		/// <param name="Value">Значение, которое хранится в контейнере</param>
		public ViewItemContainer(T Value) {
			this.Value = Value;
		}

	}

}
