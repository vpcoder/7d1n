using System;

namespace UnityEditor {

	/// <summary>
	/// Продолжает OnInspectorGUI метод у Editor объекта
	/// </summary>
	public interface IEditorEditEvent {

		/// <summary>
		/// Изменяет редактор по умолчанию
		/// </summary>
		void OnDefaultEditor();

		/// <summary>
		/// Дорисовывает дополнительный редактор
		/// </summary>
		void OnAdditionEditor();

	}

}

