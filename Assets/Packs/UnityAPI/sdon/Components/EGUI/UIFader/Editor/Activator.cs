using System;
using UnityEditor;
using UnityEngine;
using Engine.EGUI;

namespace EngineEditor.Baensi.Editors {

	public static class ActivatorFaderEditorLinks {

		[MenuItem("GameObject/UI/UIFader/AlphaFader")]
		public static bool AddAlphaFaderContextMenu() {

			if (Selection.activeTransform == null) {
				return false;
			}

			GameObject selected = Selection.activeTransform.gameObject;
			selected.AddComponent<UIFaderAlpha>();

			return true;
		}

		[MenuItem("Component/UI/UIFader/AlphaFader")]
		public static bool AddAlphaFaderMainMenu() {
			return AddAlphaFaderContextMenu();
		}

	}

}
