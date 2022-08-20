using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.Sdon
{

	public static partial class Tables {

		public static void DrawTree<T>(string title, List<T> data, ITableListeners<T> listener, bool deleteMessage = false) {

			if (data == null) {
				return;
			}

			System.Collections.ArrayList removeList = new System.Collections.ArrayList();

			EditorGUILayout.BeginHorizontal();

				GUILayout.Label(title, EditorStyles.boldLabel);

			EditorGUILayout.BeginVertical();

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

					EditorGUILayout.BeginVertical();


						EditorGUILayout.BeginHorizontal();
							if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Add),GUILayout.Width(BUTTON_SIZE),GUILayout.Height(BUTTON_SIZE))) {
								data.Insert(i+1, listener.OnConstruct()); // добавляем новый элемент в нужное место
								return;
							}
							if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Remove), GUILayout.Width(BUTTON_SIZE), GUILayout.Height(BUTTON_SIZE))) {
								if (!deleteMessage || EditorUtility.DisplayDialog("Удаление элемента", "Вы действительно хотите удалить ветку?", "Да", "Нет")) {
									removeList.Add(data[i]);
								}
							}
						EditorGUILayout.EndHorizontal();

							listener.OnEdit(data, i, data[i]); // вызываем отрисовку строчки

					EditorGUILayout.EndVertical();

					Splitter(DELIM_COLOR, 2);

				}

			EditorGUILayout.Separator();

			if (removeList.Count > 0) {
				try {
					foreach (T item in removeList)
						if (data.Contains(item))
							data.Remove(item);
				} finally {
					removeList.Clear();
				}
			}

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
			
		}

	}

}
