using System;
using UnityEngine;

namespace Engine.EGUI {

	public class UIProgressHorizontalStretch : UIProgress {

		public override void OnDrawEvent(RectTransform rect, float value) {
			rect.sizeDelta = new Vector2(Width*value, Height);
		}

	}

}
