using UnityEngine;
using System.IO;

namespace UnityEditor {

	/// <summary>
	/// Класс **APILayout** для создания специфичных полей в редакторе Unity
	/// Авторы: 7d1n
	/// Дата: 20.01.2017
	/// Версия: 1.0.0
	/// </summary>
	public static class APILayout {

		#region Hidden Fields

		/// <summary>
		/// Начало относительного пути к файлам
		/// </summary>
		private const string DEFAULT_PATH = "./Assets/";

		private static Color LABEL_BACKGROUND_COLOR = new Color(0.2f, 0.2f, 0.2f, 1f);

		private static Color LABEL_FORE_COLOR       = new Color(0f, 1f, 0f);

		private static Color INFOBOX_BACKGROUND_COLOR = new Color(0f, 0.6f, 0.9f);

		private static Color COMMENTBOX_BACKGROUND_COLOR = new Color(1.0f, 1.0f, 1.0f);

		private static Color MESSAGEBOX_BACKGROUND_COLOR = new Color(0.6f, 0.9f, 0f);

		#endregion


		#region Tools Function

		private static string getAssetsPath(string path) {
			return getDirPath(path, "/Assets/");
		}

		private static string getDirPath(string path, string dir) {
			dir = dir.Replace(".", "");
			int start = path.IndexOf(dir);
			if (start < 0) {
				return "";
			}
			return path.Substring(start + dir.Length, path.Length - start - dir.Length);
		}

		private static string getFullPath(string path) {
			return path.Replace(".", Application.dataPath.Replace("/Assets/", "/"));
		}

		#endregion


		#region Field Editors

		/// <summary>
		/// Редактор поля с сылкой на файл
		/// </summary>
		/// <param name="fileName">Предыдущее значение ссылки на файл</param>
		/// <param name="extensions">Расширение файла</param>
		/// <returns>Новое значение ссылки на файл</returns>
		public static string SettingsFileField(string fileName, string extensions = "config") {

			bool newFileMode = (fileName == null || fileName.Equals(""));
			string file = newFileMode ? "" : fileName;

			if (newFileMode && File.Exists(file)) {
				file = "<Нет>";
			} else {
				file = Path.GetFileNameWithoutExtension(file);
			}

			GUILayout.BeginHorizontal();

			GUILayout.Label("Файл:", GUILayout.Width(98), GUILayout.Height(20));

			GUI.color = new Color(0.9f, 1f, 0.95f);
			GUILayout.TextField(file);
			GUI.color = Color.white;

			if (newFileMode && GUILayout.Button("*", GUILayout.Width(20), GUILayout.Height(17))) {
				string path = EditorUtility.SaveFilePanel("Конфигурационный файл", DEFAULT_PATH, "new file name", extensions);
				if (path.Length != 0) {
					fileName = getAssetsPath(path);
					File.Create(path).Close();
				}
			}

			if (GUILayout.Button("...", GUILayout.Width(24), GUILayout.Height(17))) {
				string path = EditorUtility.OpenFilePanel("Конфигурационный файл", DEFAULT_PATH, "baensicfg");
				if (path.Length != 0)
					fileName = getAssetsPath(path);

			}

			GUILayout.EndHorizontal();

			return fileName;
		}

		/// <summary>
		/// Редактор поля с сылкой на файл 
		/// </summary>
		/// <param name="fileName">Предыдущее значение ссылки на файл</param>
		/// <param name="dir">Дирректория по умолчанию</param>
		/// <param name="dialogCaption">Метка окна диалога</param>
		/// <param name="extensions">Расширение файла</param>
		/// <returns>Новое значение ссылки на файл</returns>
		public static string FileField(string fileName, string dir = null, string dialogCaption = "Файл", string extensions = "txt") {

			string file = dir == null ? getFullPath(fileName) : getFullPath(dir + fileName) + "." + extensions;

			GUILayout.BeginHorizontal();

			if (File.Exists(file)) {
				GUI.color = new Color(0.9f, 1f, 0.95f);
			} else {
				GUI.color = new Color(1f, 0f, 0f);
			}

			fileName = GUILayout.TextField(fileName);
			GUI.color = Color.white;

			if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Open), GUILayout.Width(24), GUILayout.Height(17))) {
				string defaultPath = dir == null ? DEFAULT_PATH : dir;
				defaultPath = getFullPath(defaultPath);

				string path = EditorUtility.OpenFilePanel(dialogCaption, defaultPath, extensions);

				if (path.Length != 0) {
					fileName = getDirPath(path, defaultPath);
					fileName = fileName.Substring(0, fileName.Length - extensions.Length - 1);
				}

			}

			GUILayout.EndHorizontal();

			return fileName;

		}

		/// <summary>
		/// Строит простейший HelpBox без иконок
		/// </summary>
		/// <param name="caption">Текст сообщения на HelpBox элементе</param>
		public static void LabelBox(string caption) {
			if (caption == null) {
				caption = "";
			}
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			GUI.contentColor = LABEL_FORE_COLOR;
			GUI.backgroundColor = LABEL_BACKGROUND_COLOR;

			EditorGUILayout.HelpBox(caption, MessageType.None);
			EditorGUILayout.Separator();
			GUIAddition.ResetColors();
		}

		/// <summary>
		/// Строит информационное сообщение HelpBox
		/// </summary>
		/// <param name="caption">Текст сообщения на HelpBox элементе</param>
		public static void InfoBox(string caption) {
			if (caption == null) {
				caption = "";
			}
			GUI.color = INFOBOX_BACKGROUND_COLOR;
			EditorGUILayout.HelpBox(caption, MessageType.Info);
			GUIAddition.ResetColors();
		}

		/// <summary>
		/// Строит комментарий HelpBox
		/// </summary>
		/// <param name="caption">Текст сообщения на HelpBox элементе</param>
		public static void CommentBox(string caption) {
			if (caption == null) {
				caption = "";
			}
			GUI.color = COMMENTBOX_BACKGROUND_COLOR;
			EditorGUILayout.HelpBox(caption, MessageType.Info);
			GUIAddition.ResetColors();
		}

		/// <summary>
		/// Строит сообщение HelpBox
		/// </summary>
		/// <param name="caption">Текст сообщения на HelpBox элементе</param>
		public static void MessageBox(string caption) {
			if (caption == null) {
				caption = "";
			}
			GUI.color = MESSAGEBOX_BACKGROUND_COLOR;
			EditorGUILayout.HelpBox(caption, MessageType.Info);
			GUIAddition.ResetColors();
		}

		/// <summary>
		/// Строит сообщение HelpBox
		/// </summary>
		/// <param name="caption">Текст сообщения на HelpBox элементе</param>
		public static void WarningBox(string caption) {
			if (caption == null) {
				caption = "";
			}
			EditorGUILayout.HelpBox(caption, MessageType.Warning);
		}

		#endregion

	}

}
