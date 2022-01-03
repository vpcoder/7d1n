using System.Collections.Generic;
using UnityEditor.Sdon.I18N.Model;
using UnityEngine;
using Rotorz.ReorderableList;

namespace UnityEditor.Sdon.I18N {

	public class DatasTableListener {

		//private static Color    LINE_COLOR = new Color(0.5f,0f,0f);
		private ItemListAdapter itemListAdapter = new ItemListAdapter();

		public void AddItemSelectListener(IItemSelectListener itemSelectListener) {
			itemListAdapter.AddItemSelectListener(itemSelectListener);
		}

        private void DrawEmpty(){
            GUILayout.Label("Пусто",EditorStyles.miniLabel);
        }

        public void DrawDataView(List<Data> datas) {
            for (int i = 0; i < datas.Count; i++) {
                Data data = datas[i];

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(16f);
                EditorGUILayout.BeginVertical();

                EditorGUILayout.Separator();

				EditorGUILayout.BeginHorizontal();
					data.visible = EditorGUILayout.Foldout(data.visible, data.Name);

					if(data.Items.Count==0 && GUILayout.Button(IconsFactory.Instance.GetIcon(data.itemsVisible? Icons.Edit : Icons.EditOff), GUILayout.Width(21), GUILayout.Height(21))){
						data.itemsVisible = !data.itemsVisible;
					}

					data.Name = EditorGUILayout.TextField(data.Name,GUILayout.Width(128),GUILayout.Height(21));
				
					if (GUILayout.Button("+Регион",GUILayout.Width(64),GUILayout.Height(21))){
						
						data.Datas.Add(new Data("Data"));

					}

					if (GUILayout.Button("Удалить",GUILayout.Width(64),GUILayout.Height(21))){
						
						datas.Remove(data);

					}
				EditorGUILayout.EndHorizontal();

                if (data.visible) {

                    if (data.itemsVisible || data.Items.Count > 0) {

                        ReorderableListGUI.Title("Записи");
						itemListAdapter.SetData(data.Items);
                        ReorderableListGUI.ListField(itemListAdapter);

                    }

                    EditorGUILayout.Separator();
					
                    DrawDataView(data.Datas);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
		}

        private void DrawPath(DataPath path, List<Data> datas, Data data){
            EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Регионы: /",GUILayout.Width(96));

                DataPath iteration = path;
                for (;;) {

                    if (iteration == null) {
                        break;
                    }

                    if (iteration.data != null) {
                        iteration.data.Name = GUILayout.TextField(iteration.data.Name, GUILayout.Width(128));
                    }

                    iteration = iteration.next;
                }

                DrawDataControl(datas, data);

            EditorGUILayout.EndHorizontal();
        }

        private void DrawDataControl(List<Data> datas, Data data){

            if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Delete), GUILayout.Width(21), GUILayout.Height(21))) {

                datas.Remove(data);

            }

            if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Add), GUILayout.Width(21), GUILayout.Height(21))) {

                data.Datas.Add(new Data("Region"));

            }

            if (data.Items.Count == 0 && GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Add), GUILayout.Width(21), GUILayout.Height(21))) {

                data.Items.Add(new Item("Id","Value"));

            }

        }

	}

}
