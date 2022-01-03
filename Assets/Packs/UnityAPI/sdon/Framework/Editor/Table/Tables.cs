using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.Sdon {

	public static partial class Tables {

		private static GUIStyle splitter;

		private static Color DELIM_COLOR = new Color(0.2f, 0.2f, 0.3f);

		private const int BUTTON_SIZE = 21;
		private const int LABEL_SIZE = 24;

		public static void DrawTable<T>(string title, List<T> data, ITableListeners<T> listener, bool deleteMessage = false) {

			if (data == null) {
				return;
			}

			System.Collections.ArrayList removeList = new System.Collections.ArrayList();

			Splitter(DELIM_COLOR, 3);
			GUILayout.Label(title, EditorStyles.boldLabel);
			EditorGUILayout.Separator();
			Splitter(DELIM_COLOR, 5);

			if (data.Count == 0) {

				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("<Нет элементов>", EditorStyles.boldLabel);

					if (GUILayout.Button("Добавить")) {
						data.Add(listener.OnConstruct());
						return;
					}

				EditorGUILayout.EndHorizontal();

			} else
				for (int i = 0; i < data.Count; i++) {

					EditorGUILayout.BeginHorizontal();

					GUILayout.Label(i.ToString(), GUILayout.Width(LABEL_SIZE), GUILayout.Height(20));

					listener.OnEdit(data, i, data[i]); // вызываем отрисовку строчки

					if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Remove), GUILayout.Width(BUTTON_SIZE), GUILayout.Height(BUTTON_SIZE))) {
						if(!deleteMessage || EditorUtility.DisplayDialog("Удаление элемента", "Вы действительно хотите удалить элемент?", "Да", "Нет"))
							removeList.Add(data[i]);
					}

					if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Add),GUILayout.Width(BUTTON_SIZE),GUILayout.Height(BUTTON_SIZE))) {
						data.Insert(i+1, listener.OnConstruct()); // добавляем новый элемент в нужное место
						return;
					}

					EditorGUILayout.EndHorizontal();

					Splitter(DELIM_COLOR, 2);

				}

			EditorGUILayout.Separator();

			if (removeList.Count > 0) {
				foreach (T item in removeList)
					if(data.Contains(item))
						data.Remove(item);
				removeList.Clear();
			}

		}

		public static void DoHandlers<T>(List<T> data, ITableListeners<T> listener) {
			if(data==null)
				return;
			if (data.Count!=0)
				for(int i=0;i < data.Count; i++)
					listener.OnHandlers(data[i]);
		}


	}

}
