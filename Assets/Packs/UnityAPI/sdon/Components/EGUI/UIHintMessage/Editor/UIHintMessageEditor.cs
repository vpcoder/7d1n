using UnityEngine;
using UnityEditor;
using Engine.EGUI;
using System.Reflection;

namespace EngineEditor.Baensi.Editors {

	/// <summary>
	/// Класс-редактор для UIHintMessage
	/// </summary>
	[CustomEditor(typeof(UIHintMessage),true)]
	public class UIHintMessageEditor : CustomEditorT<UIHintMessage> {

		public override void OnStart() { }

		public override void OnDefaultEditor() {
			base.OnDefaultEditor();
		}

		public override string GetDescription() {
			return "Временное сообщение-подсказка, которая появляется в указанном месте на экране";
		}

	}

}
