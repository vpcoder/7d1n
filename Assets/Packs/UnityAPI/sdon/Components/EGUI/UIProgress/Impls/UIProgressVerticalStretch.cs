using System;
using UnityEngine;

namespace Engine.EGUI {

	public class UIProgressVerticalStretch : UIProgress {

		public override void OnDrawEvent(RectTransform rect, float value) {
			Vector3 position = rect.localPosition;

			float width  = Width;
			float height = Height * value;

			rect.sizeDelta = new Vector2(width, height);

			position.y         = height-Height;
			rect.localPosition = position;

		}

	}

}
