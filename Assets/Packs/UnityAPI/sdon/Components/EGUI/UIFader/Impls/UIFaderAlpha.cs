using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	/// <summary>
	/// Анимация исчезания UI панелей с помощью настройки прозрачности
	/// </summary>
	public class UIFaderAlpha : UIFader {

		/// <summary>
		/// Перерисовывает UI слой относительно заданной альфы (**Вызывается только в том случае, когда WidgetGraphic не равен null**, тоесть, this.GetComponent<Graphic>()!=null)
		/// </summary>
		/// <param name="rect">RectTransform объект, для которого совершается перерисовка анимации</param>
		/// <param name="graphic">Graphic объект, для которого совершается перерисовка анимации</param>
		/// <param name="visible">Значение видимости от 0f до 1f</param>
		/// <param name="isChild">Флаг показывает, является ли текущий RectTransform дочерним (true) или родительским (false)</param>
		public override void OnDrawEvent(RectTransform rect, Graphic graphic, float visible, bool isChild) {
			
			Color color = graphic.color;
			float value = minVisible + visible * (maxVisible - minVisible);

			color.a       = value;
			graphic.color = color;

		}

	}

}
