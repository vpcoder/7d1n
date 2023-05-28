using UnityEditor;
using UnityEngine;
using Engine.EGUI;
using UnityEngine.UI;

namespace EngineEditor.Baensi.Editors {

	public static class ActivatorHintMessageEditorLinks {

		[MenuItem("GameObject/UI/UIHintMessage/UIHintMessageWorldPosition")]
		public static bool AddHintMessageWorldPositionContextMenu()
		{

			Canvas canvas = ObjectFinder.Canvas;

			GameObject    newObject = new GameObject();
			RectTransform parent = newObject.AddComponent<RectTransform>();
			parent.name = "HintMessage";

			if (Selection.activeTransform == null) {
				parent.SetParent(canvas.transform);
			} else {
				parent.SetParent(Selection.activeTransform);
			}

			parent.SetAnchorPreset(AnchorPresets.LeftTop);
			parent.pivot = new Vector2(0, 1);

			ContentSizeFitter parentFitter = newObject.AddComponent<ContentSizeFitter>();
			parentFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			parentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			VerticalLayoutGroup parentGroup = newObject.AddComponent<VerticalLayoutGroup>();
			parentGroup.childControlHeight     = true;
			parentGroup.childControlWidth      = true;
			parentGroup.childForceExpandHeight = false;
			parentGroup.childForceExpandWidth  = false;

			Image image = newObject.AddComponent<Image>();
			image.color = new Color(0.1f, 0.1f, 0.1f);

			GameObject textFieldObject = new GameObject();
			RectTransform textFieldRect = textFieldObject.AddComponent<RectTransform>();
			textFieldRect.name = "Text";
			textFieldRect.SetParent(parent);
			textFieldRect.SetAnchorPreset(AnchorPresets.FillFull);
			textFieldRect.pivot         = Vector2.zero;
			textFieldRect.offsetMin     = Vector2.zero;
			textFieldRect.offsetMax     = Vector2.zero;
			textFieldRect.rotation      = Quaternion.identity;
			textFieldRect.localScale    = Vector3.one;

			Text textField = textFieldObject.AddComponent<Text>();
			textField.text = "Hint Message Text";
			textField.alignment = TextAnchor.MiddleCenter;
			textField.verticalOverflow = VerticalWrapMode.Overflow;
			textField.raycastTarget = false;

			UIFaderAlpha fader = newObject.AddComponent<UIFaderAlpha>();
			ReflectorHandler.SetFieldValue<UIFader, float>(fader, "fadeSpeed", 0.2f);
			ReflectorHandler.SetFieldValue<UIFader, bool>(fader, "disableChilds", false);
			ReflectorHandler.SetFieldValue<UIFader, bool>(fader, "fadeCascade", true);
			textFieldObject.SetActive(true);
			

			UIHintMessageWorldPosition hintMessage = parent.gameObject.AddComponent<UIHintMessageWorldPosition>();
			hintMessage.Position = new Vector3(0, 0, 0);
			hintMessage.Size     = new Vector2(130, 0);

			ReflectorHandler.SetFieldValue<UIHintMessageWorldPosition, Text>(hintMessage, "textField", textField);
			hintMessage.Border.Set(8, 8, 8, 8);
			newObject.layer = 0;

			Canvas.ForceUpdateCanvases();
			
			return true;
		}

		[MenuItem("GameObject/UI/UIHintMessage/UIHintMessageScreenPosition")]
		public static bool AddHintMessageScreenPositionContextMenu()
		{

			Canvas canvas = ObjectFinder.Canvas;

			GameObject newObject = new GameObject();
			RectTransform parent = newObject.AddComponent<RectTransform>();
			parent.name = "HintMessage";

			if (Selection.activeTransform == null) {
				parent.SetParent(canvas.transform);
			} else {
				parent.SetParent(Selection.activeTransform);
			}

			parent.SetAnchorPreset(AnchorPresets.LeftTop);
			parent.pivot = new Vector2(0, 1);

			Image image = newObject.AddComponent<Image>();
			image.color = new Color(0, 0.5f, 0);

			ContentSizeFitter parentFitter = newObject.AddComponent<ContentSizeFitter>();
			parentFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			parentFitter.verticalFit   = ContentSizeFitter.FitMode.PreferredSize;

			VerticalLayoutGroup parentGroup = newObject.AddComponent<VerticalLayoutGroup>();
			parentGroup.childControlHeight     = true;
			parentGroup.childControlWidth      = true;
			parentGroup.childForceExpandHeight = false;
			parentGroup.childForceExpandWidth  = false;

			GameObject textFieldObject = new GameObject();
			RectTransform textFieldRect = textFieldObject.AddComponent<RectTransform>();
			textFieldRect.name = "Text";
			textFieldRect.SetParent(parent);
			textFieldRect.SetAnchorPreset(AnchorPresets.FillFull);
			textFieldRect.pivot = Vector2.zero;
			textFieldRect.offsetMin = Vector2.zero;
			textFieldRect.offsetMax = Vector2.zero;
			textFieldRect.rotation = Quaternion.identity;
			textFieldRect.localScale = Vector3.one;

			Text textField = textFieldObject.AddComponent<Text>();
			textField.text = "Hint Message Text";
			textField.alignment = TextAnchor.MiddleCenter;
			textField.verticalOverflow = VerticalWrapMode.Overflow;
			textField.raycastTarget = false;
			
			UIFaderAlpha fader = newObject.AddComponent<UIFaderAlpha>();
			ReflectorHandler.SetFieldValue<UIFader, float>(fader, "fadeSpeed", 0.2f);
			ReflectorHandler.SetFieldValue<UIFader, bool>(fader, "disableChilds", false);
			ReflectorHandler.SetFieldValue<UIFader, bool>(fader, "fadeCascade", true);
			textFieldObject.SetActive(true);
			
			UIHintMessageScreenPosition hintMessage = parent.gameObject.AddComponent<UIHintMessageScreenPosition>();
			hintMessage.Position = new Vector3(30, 30, 0);
			hintMessage.Size = new Vector2(130, 0);

			ReflectorHandler.SetFieldValue<UIHintMessageScreenPosition, Text>(hintMessage, "textField", textField);
			hintMessage.Border.Set(8, 8, 8, 8);
			newObject.layer = LayerMask.NameToLayer("UI");

			Canvas.ForceUpdateCanvases();

			return true;
		}

		[MenuItem("Component/UI/UIHintMessage/UIHintMessageWorldPosition")]
		public static bool AddHintMessageWorldPositionMainMenu() {
			return AddHintMessageWorldPositionContextMenu();
		}

		[MenuItem("Component/UI/UIHintMessage/UIHintMessageScreenPosition")]
		public static bool AddHintMessageScreenPositionMainMenu() {
			return AddHintMessageScreenPositionContextMenu();
		}

	}

}
