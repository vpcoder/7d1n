using System;
using System.Collections.Generic;

namespace Engine.EGUI {

	/// <summary>
	/// Класс хандлер, управляющий контейнерами
	/// </summary>
	public static class ViewerItemContainerHandlers {

		/// <summary>
		/// Оборачивает массив структур/енумов в массив контейнеров
		/// </summary>
		/// <param name="values">Массив структур/енумов</param>
		/// <typeparam name="T">Тип структуры/енума</typeparam>
		public static IViewItemContainer<T>[] Convert<T>(T[] values) where T : struct {
			List<IViewItemContainer<T>> result = new List<IViewItemContainer<T>>();
				foreach(T value in values) {
					result.Add(new ViewItemContainer<T>(value));
				}
			return result.ToArray();
		}

		/// <summary>
		/// Оборачивает структуру/енум в контейнер
		/// </summary>
		/// <param name="value">Структура/енум</param>
		/// <typeparam name="T">Тип структуры/енума</typeparam>
		public static IViewItemContainer<T> Convert<T>(T value) where T : struct {
			return new ViewItemContainer<T>(value);
		}

	}

}
