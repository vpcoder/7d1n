using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Упрощённая модель проводника UI композитов, в которой нет дочерних элементов
	/// </summary>
	/// <typeparam name="T">Тип элемента списка</typeparam>
	public abstract class ITableCompositeProvider<T> : ICompositeProvider<T>
		where T : class {

		/// <summary>
		/// Формирует и возвращает UI композит для заданного элемента списка
		/// </summary>
		/// <returns>Сформированный UI композит</returns>
		/// <param name="item">Элемент списка, для которого необходимо сформировать UI композит</param>
		public abstract RectTransform GetComposite(T item);

		/// <summary>
		/// В Table модели метод не несёт физического смысла
		/// </summary>
		[Obsolete("Не работает для Table модели",false)]
		public RectTransform GetChildComposite(T child){
			return null;
		}

	}

}
