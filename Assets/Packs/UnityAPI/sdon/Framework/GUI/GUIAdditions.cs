
namespace UnityEngine {

	/// <summary>
	/// Вспомогательный GUI класс для работы с UnityEngine.GUI
	/// </summary>
	public static class GUIAddition {

		private static Color DEFAULT_COLOR = Color.white;

		/// <summary>
		/// Сбрасывает цвета GUI в значение по умолчанию
		/// </summary>
		public static void ResetColors() {
			GUI.color           = DEFAULT_COLOR;
			GUI.contentColor    = DEFAULT_COLOR;
			GUI.backgroundColor = DEFAULT_COLOR;
		}

	}

}
