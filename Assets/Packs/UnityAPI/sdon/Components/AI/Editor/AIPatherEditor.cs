using UnityEngine;
using UnityEditor;
using Engine.Enemy;

namespace EngineEditor.Baensi.Editors {

	[CustomEditor(typeof(AIPather),true)]
	public class AIPatherEditor : CustomEditorT<AIPather> {

		public override string GetDescription() {
			return "Аниматор UI панелей\nКласс позволяет анимированно отображать/скрывать UI слой";
		}

	}

}
