using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Определяет проводника контента
	/// </summary>
	/// <typeparam name="T">Тип элемента списка</typeparam>
	public interface IContentProvider<T>
		where T : class  {

		/// <summary>
		/// Формирует список элементов по входному аргументу, и возвращает этот список
		/// </summary>
		/// <param name="input">Входные данные, на основании которых будет сформирован контент (список данных)</param>
		/// <returns>Сформированный массив данных по входному аргументу</returns>
		T[] GetElements(object input);

		/// <summary>
		/// Определяет, содержит ли элемент item дочерние элементы
		/// </summary>
		/// <param name="item">элемент списка</param>
		/// <returns>Возвращает логическое значение, содержит ли элемент дочерние элементы.</returns>
		bool HasChilds(T item);

		/// <summary>
		/// Возвращает список дочерних элементов
		/// </summary>
		/// <param name="item">Элемент списка у которого извлекаются дочерние элементы</param>
		/// <returns>Список дочерних элементов</returns>
		T[] GetChilds(T item);

		/// <summary>
		/// Возвращает родительский элемент по дочернему
		/// </summary>
		/// <param name="child">Дочерний элемент</param>
		/// <returns>Родительский элемент</returns>
		T GetParent(T child);
		
	}

}
