using UnityEngine;
using UnityEditor;
using Engine.EGUI;
using System.Reflection;

namespace EngineEditor.Baensi.Editors {

	/// <summary>
	/// Класс-редактор для UIProgress-а
	/// </summary>
	[CustomEditor(typeof(UIProgress), true)]
	public class UIProgressEditor : CustomEditorT<UIProgress> {

		public override void OnStart() { }

		public override void OnDefaultEditor() {
			base.OnDefaultEditor();
		}

		public override string GetDescription() {
			return "UI прогрессбар";
		}

	}

}
