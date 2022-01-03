using Engine.EGUI;
using UnityEditor;
using UnityEngine.UI;
using Engine.I18N;

namespace EngineEditor.Baensi.Editors {

	[CustomEditor(typeof(I18NText), true)]
	public class I18NTextEditor : CustomEditorT<I18NText> {

		private SerializedProperty textId;

		public override void OnStart() {
			textId = target.GetFieldSerialized("textId");
		}

		public override void OnDefaultEditor() {
			textId.stringValue = APILayout.I18NTextField(textId.stringValue);
			target.Target.GetComponent<Text>().text = CLang.getInstance().tryGet(textId.stringValue, true);
		}

		public override string GetDescription() {
			return "Интернацианализатор значения содержимого UI.Text компонента";
		}

	}

}
