using System;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **RectTransformAddition** для RectTransform объекта Юнити 
    /// Авторы: sdon
    /// Дата: 15.02.2017
    /// Версия: 1.0.0
    /// </summary>
    public static class RectTransformAddition {

		private static void SetAnchorLeftTop(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 1f);
			rect.anchorMax = new Vector2(0f, 1f);
		}

		private static void SetAnchorMiddleTop(this RectTransform rect) {
			rect.anchorMin = new Vector2(0.5f, 1f);
			rect.anchorMax = new Vector2(0.5f, 1f);
		}

		private static void SetAnchorRightTop(this RectTransform rect) {
			rect.anchorMin = new Vector2(1f, 1f);
			rect.anchorMax = new Vector2(1f, 1f);
		}

		private static void SetAnchorLeftCenter(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 0.5f);
			rect.anchorMax = new Vector2(0f, 0.5f);
		}

		private static void SetAnchorMiddleCenter(this RectTransform rect) {
			rect.anchorMin = new Vector2(0.5f, 0.5f);
			rect.anchorMax = new Vector2(0.5f, 0.5f);
		}

		private static void SetAnchorRightCenter(this RectTransform rect) {
			rect.anchorMin = new Vector2(1f, 0.5f);
			rect.anchorMax = new Vector2(1f, 0.5f);
		}

		private static void SetAnchorLeftBottom(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(0f, 0f);
		}

		private static void SetAnchorMiddleBottom(this RectTransform rect) {
			rect.anchorMin = new Vector2(0.5f, 0f);
			rect.anchorMax = new Vector2(0.5f, 0f);
		}

		private static void SetAnchorRightBottom(this RectTransform rect) {
			rect.anchorMin = new Vector2(1f, 0f);
			rect.anchorMax = new Vector2(1f, 0f);
		}

		private static void SetAnchorFillTop(this RectTransform rect) {
			rect.anchorMin = new Vector2(1f, 0f);
			rect.anchorMax = new Vector2(1f, 1f);
		}

		private static void SetAnchorFillCenter(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 0.5f);
			rect.anchorMax = new Vector2(1f, 0.5f);
		}

		private static void SetAnchorFillBottom(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(1f, 0f);
		}

		private static void SetAnchorLeftFill(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(0f, 1f);
		}

		private static void SetAnchorMiddleFill(this RectTransform rect) {
			rect.anchorMin = new Vector2(0.5f, 0f);
			rect.anchorMax = new Vector2(0.5f, 1f);
		}

		private static void SetAnchorRightFill(this RectTransform rect) {
			rect.anchorMin = new Vector2(1f, 0f);
			rect.anchorMax = new Vector2(1f, 1f);
		}

		private static void SetAnchorFillFull(this RectTransform rect) {
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(1f, 1f);
		}

		public static void SetAnchorPreset(this RectTransform rect, AnchorPresets preset) {
			switch (preset) {
				case AnchorPresets.LeftTop:
					SetAnchorLeftTop(rect);
					break;
				case AnchorPresets.MiddleTop:
					SetAnchorMiddleTop(rect);
					break;
				case AnchorPresets.RightTop:
					SetAnchorRightTop(rect);
					break;
				case AnchorPresets.LeftCenter:
					SetAnchorLeftCenter(rect);
					break;
				case AnchorPresets.MiddleCenter:
					SetAnchorMiddleCenter(rect);
					break;
				case AnchorPresets.RightCenter:
					SetAnchorRightCenter(rect);
					break;
				case AnchorPresets.LeftBottom:
					SetAnchorLeftBottom(rect);
					break;
				case AnchorPresets.MiddleBottom:
					SetAnchorMiddleBottom(rect);
					break;
				case AnchorPresets.RightBottom:
					SetAnchorRightBottom(rect);
					break;
				case AnchorPresets.FillTop:
					SetAnchorFillTop(rect);
					break;
				case AnchorPresets.FillCenter:
					SetAnchorFillCenter(rect);
					break;
				case AnchorPresets.FillBottom:
					SetAnchorFillBottom(rect);
					break;
				case AnchorPresets.LeftFill:
					SetAnchorLeftFill(rect);
					break;
				case AnchorPresets.MiddleFill:
					SetAnchorMiddleFill(rect);
					break;
				case AnchorPresets.RightFill:
					SetAnchorRightFill(rect);
					break;
				case AnchorPresets.FillFull:
					SetAnchorFillFull(rect);
					break;
			}
		}

	}

}
