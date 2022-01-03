using System;

namespace Engine.EGUI {

	/// <summary>
	/// Определяет провайдера контента для одномерной таблицы
	/// </summary>
	/// <typeparam name="T">Тип элемента списка</typeparam>
	public abstract class ITableContentProvider<T> : IContentProvider<T>
		where T : class {

		/// <summary>
		/// Формирует список элементов по входному аргументу, и возвращает этот список
		/// </summary>
		/// <param name="input">Входные данные, на основании которых будет сформирован контент (список данных)</param>
		/// <returns>Сформированный массив данных по входному аргументу</returns>
		public abstract T[] GetElements(object input);

		/// <summary>
		/// Определяет, содержит ли элемент item дочерние элементы
		/// </summary>
		/// <param name="item">элемент списка</param>
		/// <returns>Возвращает логическое значение, содержит ли элемент дочерние элементы.</returns>
		[Obsolete("Не работает для Table модели",false)]
		public bool HasChilds(T item){
			return false;
		}

		/// <summary>
		/// Возвращает список дочерних элементов
		/// </summary>
		/// <param name="item">Элемент списка у которого извлекаются дочерние элементы</param>
		/// <returns>Список дочерних элементов</returns>
		[Obsolete("Не работает для Table модели",false)]
		public T[] GetChilds(T item){
			return null;
		}

		/// <summary>
		/// Возвращает родительский элемент по дочернему
		/// </summary>
		/// <param name="child">Дочерний элемент</param>
		/// <returns>Родительский элемент</returns>
		[Obsolete("Не работает для Table модели",false)]
		public T GetParent(T child){
			return null;
		}

	}

}
