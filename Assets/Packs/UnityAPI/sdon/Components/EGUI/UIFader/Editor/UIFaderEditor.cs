using UnityEngine;
using UnityEditor;
using Engine.EGUI;
using System.Reflection;

namespace EngineEditor.Baensi.Editors {

	/// <summary>
	/// Класс-редактор для UIFader-а
	/// </summary>
	[CustomEditor(typeof(UIFader),true)]
	public class UIFaderEditor : CustomEditorT<UIFader> {

		public override void OnStart() { }

		public override void OnDefaultEditor() {
			base.OnDefaultEditor();
		}

		public override void OnAdditionEditor() {

			EditorGUILayout.Separator();

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Показать")) {
				target.Target.OnShow();
			}

			if (GUILayout.Button("Скрыть")) {
				target.Target.OnHide();
			}

			GUILayout.EndHorizontal();

		}

		public override string GetDescription() {
			return "Аниматор UI панелей\nКласс позволяет анимированно отображать/скрывать UI слой";
		}

	}

}
