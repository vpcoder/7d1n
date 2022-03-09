using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor {

	/// <summary>
	/// Вспомогательный класс для редактора Unity. Позволяет получать дополнительные иконки
	/// Авторы: 7d1n
	/// Дата: 15.01.2017
	/// Версия: 1.0.0
	/// </summary>
	public class IconsFactory {

		#region Hidden Fields

		/// <summary>
		/// Устанавливает жёсткий путь к папке с иконками внутри исходников UnityAPI
		/// В C#4.5 было бы возможно решение через рефлексию (Атрибут [CallerFilePath])
		/// </summary>
		private const string PATH = "./Assets/Packs/UnityAPI/sdon/Framework/Editor/Icons/data/";

		/// <summary>
		/// Словарь с иконками
		/// </summary>
		private readonly SortedDictionary<Icons, Texture2D> data;

        #endregion

        private static readonly Lazy<IconsFactory> instance = new Lazy<IconsFactory>(() => new IconsFactory());
        public static IconsFactory Instance { get { return instance.Value; } }


        /// <summary>
        /// Загружает иконку, находящуюся в папке "data" этого *.cs файла
        /// </summary>
        /// <param name="path">Имя иконки в папке "data"</param>
        /// <returns>Возвращает иконку в виде Texture2D</returns>
        private Texture2D loadIcon(string path) {
			return LoadIcon(PATH+path);
        }

		public static Texture2D LoadIcon(string fullpath) {
			Texture2D result = new Texture2D(1, 1);
				result.LoadImage(System.IO.File.ReadAllBytes(fullpath));
			result.Apply();
			return result;
		}

		public IconsFactory() {

			data = new SortedDictionary<Icons, Texture2D>();

			data.Add(Icons.Empty,         loadIcon("empty.png"));
								    
			data.Add(Icons.Delete,        loadIcon("delete.png"));
			data.Add(Icons.Add,           loadIcon("add.png"));
			data.Add(Icons.Remove,        loadIcon("remove.png"));
			data.Add(Icons.Edit,          loadIcon("edit.png"));
			data.Add(Icons.EditOff,       loadIcon("edit_off.png"));

			data.Add(Icons.Save,          loadIcon("save.png"));
			data.Add(Icons.SaveOff,       loadIcon("save_off.png"));
			data.Add(Icons.Open,          loadIcon("open.png"));
			data.Add(Icons.Refresh,       loadIcon("refresh.png"));

			data.Add(Icons.ClipboardCopy, loadIcon("clipboard_copy.png"));

			data.Add(Icons.Info,          loadIcon("info.png"));
			data.Add(Icons.InfoOff,       loadIcon("info_off.png"));

			data.Add(Icons.Error,         loadIcon("error.png"));
		}

        /// <summary>
        /// Возвращает изображение указанной иконки
        /// </summary>
        /// <param name="code">Код иконки из UnityEngine.Baensi.Icons enum-а</param>
        /// <returns>Возвращает изображение иконки в формате Texture2D</returns>
        /// <exception>System.Collections.Generic.KeyNotFoundException</exception>
        /// <example>
        /// Пример использования:
        /// <code>
        /// if(GUILayout.Button(IconsFactory.Instance.getIcon(Icons.Close), GUILayout.Width(24), GUILayout.Height(17))) {
        ///		...
        /// }
        /// </code>
        /// </example>
        public Texture2D GetIcon(Icons code) {

			if(!data.TryGetValue(code, out Texture2D result))
            {
                Debug.LogError("Указанной иконки не существует!");
                throw new KeyNotFoundException();
            }

			return result;
		}

	}

}
