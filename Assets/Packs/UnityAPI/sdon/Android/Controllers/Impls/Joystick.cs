using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Engine.IO
{

	/// <summary>
	/// Класс джойсика для мобильных проектов
	/// </summary>
	public class Joystick : IODeviceBase, IDragHandler, IPointerUpHandler, IPointerDownHandler
	{

		#region Shared Fields

		[SerializeField]
		private int joystickHandleDistance = 5;

		[SerializeField]
		private Image backgroundImage;

		[SerializeField]
		private Image joystickKnobImage;

		#endregion

		public override void OnStart()
		{
			base.OnStart();
			backgroundImage.rectTransform.pivot = new Vector2(1, 0);
			backgroundImage.rectTransform.anchorMin = new Vector2(0, 0);
			backgroundImage.rectTransform.anchorMax = new Vector2(0, 0);
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			Vector2 localPoint = Vector2.zero;

			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform,
																		 eventData.position,
																		 eventData.pressEventCamera,
																		 out localPoint))
			{
				return;
			}

			localPoint.x = (localPoint.x / backgroundImage.rectTransform.sizeDelta.x);
			localPoint.y = (localPoint.y / backgroundImage.rectTransform.sizeDelta.y);

			inputVector = new Vector3(localPoint.x * 2 + 1, localPoint.y * 2 - 1, 0);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joystickKnobImage.rectTransform.anchoredPosition = new Vector3(
						 inputVector.x * (backgroundImage.rectTransform.sizeDelta.x / joystickHandleDistance),
						 inputVector.y * (backgroundImage.rectTransform.sizeDelta.y / joystickHandleDistance));

			OnChangeListener();

			EventSystem.current.IsPointerOverGameObject();

		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			OnDrag(eventData);
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			inputVector = Vector3.zero;
			joystickKnobImage.rectTransform.anchoredPosition = Vector3.zero;
			OnChangeListener();
		}

		public override void OnChangeListener()
		{
		}

	}

}
