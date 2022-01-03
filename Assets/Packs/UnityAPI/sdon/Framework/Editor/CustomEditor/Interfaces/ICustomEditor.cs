
namespace UnityEditor {

	/// <summary>
	/// Интерфейс пользовательского редактора
	/// </summary>
	public interface ICustomEditor : IEditorStartEvent, IEditorEditEvent {

		/// <summary>
		/// Метод выполняет построение раздела документации в редакторе
		/// </summary>
		void OnDocsEditor();

		/// <summary>
		/// Возвращает описание редактора (CustomEditor-а)
		/// </summary>
		string GetDescription();

		/// <summary>
		/// Возвращает текст информации для редактора
		/// </summary>
		string GetTextInfo();

		/// <summary>
		/// Тип видимости редактора по умолчанию (редактор для класса, который формирует Unity по SerializeField/public полям)
		/// </summary>
		EditorVisibleType GetEditorVisibleType();

	}

}
