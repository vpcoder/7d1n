using System;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс промежуточного класса-контейнера, который позволит передавать структуры и enum-ы внутрь провайдеров
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IViewItemContainer<T> where T : struct {

		/// <summary>
		/// Свойство-поле с данными
		/// </summary>
		T Value {
			get;
			set;
		}

	}

}
