using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.IO
{

	/// <summary>
	/// Класс джойстика, считывающего направление относительно центра экрана в диапазоне [0,1] по каждой из осей
	/// </summary>
	public class CameraDirectionJoystik : IODeviceBase, IPointerDownHandler, IDragHandler
	{

		/// <summary>
		/// Направление в полярной системе координат
		/// </summary>
		private float angleDegrees;

		public void OnDrag(PointerEventData eventData)
		{
			UpdateTouch(eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			UpdateTouch(eventData);
		}

		private void UpdateTouch(PointerEventData eventData)
		{
			Vector2 cursorPosition = eventData.position;
			this.inputVector.x = cursorPosition.x - Screen.width * 0.5f;
			this.inputVector.y = cursorPosition.y - Screen.height * 0.5f; // значение направления в декартовой системе координат
			OnChangeListener();
		}

		public override void OnChangeListener()
		{
		}

		/// <summary>
		/// Возвращает поворот по оси Y в сторону нажатия пальцем на экране
		/// Вектор поворота перпендикулярен плоскости проекции экрана
		/// </summary>
		public Quaternion ToQuaternion
		{
			get
			{
				float angleRadians = Mathf.Atan2(this.inputVector.y, this.inputVector.x);
				angleDegrees = 180 - angleRadians * Mathf.Rad2Deg;
				return Quaternion.Euler(new Vector3(0, angleDegrees, 0));
			}
		}

	}

}
