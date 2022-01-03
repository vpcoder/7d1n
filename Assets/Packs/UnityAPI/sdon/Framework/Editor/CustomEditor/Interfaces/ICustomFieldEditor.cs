using System;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Общий интерфейс для пользовательских редакторов полей
	/// </summary>
	public interface ICustomFieldEditor {

		/// <summary>
		/// Показывает, подходит ли указанный тип для редактирования текущим редактором полей
		/// </summary>
		/// <param name="type">Тип данных, которые необходимо отредактировать</param>
		/// <returns>Логическое значение, подходит ли указанный тип для редактирования текущим редактором полей</returns>
		bool IsEditorType(Type type);

		/// <summary>
		/// Выполняет построение редактора поля
		/// </summary>
		/// <param name="target">Экземпляр целевого объекта</param>
		/// <param name="field">Метаданные поля класса целевого объекта</param>
		/// <param name="title">Заголовок редактируемого поля целевого объекта</param>
		void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener);

	}

}
