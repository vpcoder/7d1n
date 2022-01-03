using UnityEngine;

namespace UnityEditor.Sdon.I18N {

	/// <summary>
	/// Активатор редактора словаря
	/// </summary>
	public static class Activator {

		public static string I18N_DATA = "I18NEnviroment";

		[MenuItem("[Sdon]/Редактор словаря")]
		public static void ShowWindow() {
			EditorWindow window = EditorWindow.GetWindow(typeof(DictionaryEditorWindow));
			window.titleContent = new GUIContent("I18N Словарь");
		}

	}

}
