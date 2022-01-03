using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс viewer-а списка данных
	/// </summary>
	/// <typeparam name="T">Тип элемента списка</typeparam>
	public interface IViewer<T> : IWidget, IInputProvider
		where T : class {
	
		/// <summary>
		/// Возвращает/Устанавливает проводника контента
		/// </summary>
		/// <returns>Проводник контента</returns>
		IContentProvider<T> ContentProvider {
			get;
			set;
		}
			
		/// <summary>
		/// Возвращает/Устанавливает проводника UI композитов
		/// </summary>
		/// <returns>Проводник UI композитов</returns>
		ICompositeProvider<T> CompositeProvider {
			get;
			set;
		}

	}

}
