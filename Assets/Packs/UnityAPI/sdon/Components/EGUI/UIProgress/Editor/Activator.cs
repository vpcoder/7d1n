using System;
using UnityEditor;
using UnityEngine;
using Engine.EGUI;
using UnityEngine.UI;

namespace EngineEditor.Baensi.Editors {

	public static class ActivatorProgressEditorLinks {

		private static RectTransform createProgressBase<T>(string name, float width, float height) where T : UIProgress {
			GameObject selected = Selection.activeTransform.gameObject;

			GameObject progress = new GameObject();
			progress.transform.SetParent(selected.transform);
			progress.transform.name = name;
			RectTransform rect = progress.AddComponent<RectTransform>();
			rect.SetAnchorPreset(AnchorPresets.LeftTop);
			rect.pivot = new Vector2(0, 1);

			GameObject background = new GameObject();
			RectTransform backgroundRect = background.AddComponent<RectTransform>();
			Image backgroundImage = background.AddComponent<Image>();
			backgroundImage.raycastTarget = false;
			backgroundImage.color = new Color(1, 1, 1);
			background.transform.name = "background";
			background.transform.SetParent(progress.transform);
			backgroundRect.SetAnchorPreset(AnchorPresets.LeftTop);
			backgroundRect.pivot = new Vector2(0, 1);

			GameObject fill = new GameObject();
			RectTransform fillRect = fill.AddComponent<RectTransform>();
			Image fillImage = fill.AddComponent<Image>();
			fillImage.raycastTarget = false;
			fillImage.color = new Color(1, 0, 0);
			fill.transform.name = "progress";
			fill.transform.SetParent(progress.transform);
			
			T uiprogress = progress.AddComponent<T>();
			uiprogress.Rect = fillRect;
			uiprogress.Background = backgroundRect;

			rect.localPosition = new Vector3(0, 0, 0);
			rect.sizeDelta     = new Vector2(width, height);

			backgroundRect.localPosition = new Vector3(0, 0, 0);
			fillRect.localPosition = new Vector3(0, 0, 0);

			uiprogress.Value = 0.35f;

			return fillRect;
		}
	
		[MenuItem("GameObject/UI/UIProgress/HorizontalStretchProgress")]
		public static bool AddHorizontalStretchProgressContextMenu() {
			if (Selection.activeTransform == null) {
				return false;
			}
			RectTransform fillRect = createProgressBase<UIProgressHorizontalStretch>("H Progress", 320, 24);
			fillRect.SetAnchorPreset(AnchorPresets.LeftTop);
			fillRect.pivot = new Vector2(0, 1);
			fillRect.localPosition = new Vector3(0, 0, 0);
			return true;
		}

		[MenuItem("Component/UI/UIProgress/HorizontalStretchProgress")]
		public static bool AddHorizontalStretchProgressMainMenu() {
			return AddHorizontalStretchProgressContextMenu();
		}
		
		
		[MenuItem("GameObject/UI/UIProgress/VerticalStretchProgress")]
		public static bool AddVerticalStretchProgressContextMenu() {
			if (Selection.activeTransform == null) {
				return false;
			}
			RectTransform fillRect = createProgressBase<UIProgressVerticalStretch>("V Progress",24,320);
			fillRect.SetAnchorPreset(AnchorPresets.LeftBottom);
			fillRect.pivot = new Vector2(0, 1);
			fillRect.localPosition = new Vector3(0, 0, 0);
			return true;
		}

		[MenuItem("Component/UI/UIProgress/VerticalStretchProgress")]
		public static bool AddVerticalStretchProgressMainMenu() {
			return AddVerticalStretchProgressContextMenu();
		}

	}

}
