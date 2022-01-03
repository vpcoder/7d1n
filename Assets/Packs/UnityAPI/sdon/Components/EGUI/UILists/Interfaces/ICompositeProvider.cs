using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Определяет проводника UI комопзитов
	/// </summary>
	public interface ICompositeProvider<T>
		where T : class {

		/// <summary>
		/// Формирует и возвращает UI композит для заданного элемента списка
		/// </summary>
		/// <returns>Сформированный UI композит</returns>
		/// <param name="item">Элемент списка, для которого необходимо сформировать UI композит</param>
		RectTransform GetComposite(T item);

		/// <summary>
		/// Формирует и возвращает UI композит для заданного дочернего элемента списка
		/// </summary>
		/// <returns>Сформированный UI композит</returns>
		/// <param name="child">Дочерний элемент списка, для которого необходимо сформировать UI композит</param>
		RectTransform GetChildComposite(T child);

	}

}
