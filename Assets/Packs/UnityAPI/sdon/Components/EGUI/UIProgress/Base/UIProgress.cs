using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public abstract class UIProgress : MonoBehaviour, IProgress {

		#region Shared Fields

		[Caption("Rect Прогресса")]
		[Comments("Ссылка на RectTransform компонент прогресса")]
		[SerializeField] private RectTransform rect;

		[Caption("Rect Задника")]
		[Comments("Ссылка на RectTransform компонент задника прогресса")]
		[SerializeField] private RectTransform background;

		[Caption("Прогресс")]
		[Comments("Показывает установленное значение прогресса")]
		[SerializeField] [Range(0f,1f)] protected float value = 0f;

		[Caption("Скорость")]
		[Comments("Устанавливает скорость интерполяции между текущим и установленным значение прогресса")]
		[SerializeField] private float speed = 10f;

		[Caption("Ширина")]
		[Comments("Устанавливает ширину полосы прогресса")]
		[SerializeField] private float width = 120f;

		[Caption("Высота")]
		[Comments("Устанавливает высоту полосы прогресса")]
		[SerializeField] private float height = 20f;
		
		#endregion

		#region Hide Fields

		private float currentValue;

		private RectTransform thisRect;

		/// <summary>
		/// Показывает минимальный порог разности между значениями (чувствительности)
		/// </summary>
		private const float SENSETIVITY_STEP = 0.0005f;

		#endregion

		#region Unity Events

		private void Start() {
			thisRect     = GetComponent<RectTransform>();
			currentValue = value;
			OnStart();
		}

		private void Update() {

#if UNITY_EDITOR

			if(Application.isPlaying) {

#endif

				if (Mathf.Abs(value - currentValue) <= SENSETIVITY_STEP) {
					return;
				}

#if UNITY_EDITOR

			}

#endif

			Vector2 size = thisRect.sizeDelta;
			Vector3 pos  = background.localPosition;

			if (size.x < 0) {
				size.x = (Screen.width + size.x - pos.x * 2);
			}
			if (size.y < 0) {
				size.y = (Screen.height + size.y - pos.y * 2);
			}

			width  = size.x;
			height = size.y;

			background.sizeDelta = new Vector2(width, height);

			currentValue = Mathf.Lerp(currentValue, value, Time.deltaTime * speed);

			OnDrawEvent(rect, currentValue);

		}

		#endregion

		/// <summary>
		/// Устанавливает/Возвращает текущее значение прогресса
		/// </summary>
		public float Value {
			get {
				return this.value;
			}
			set {
				this.value = value;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает ширину полосы прогресса
		/// </summary>
		public float Width {
			get {
				return this.width;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает высоту полосы прогресса
		/// </summary>
		public float Height {
			get {
				return this.height;
			}
		}

		public virtual void OnStart() { }
		public virtual void OnDrawEvent(RectTransform rect, float value) { }


		#region Widget

		/// <summary>
		/// Отступы границ виджета
		/// </summary>
		/// <value>Границы</value>
		public IBorder Border {
			get;
			set;
		}

		/// <summary>
		/// RectTransform виджета
		/// </summary>
		/// <value>Возвращает объект RectTransform виджета</value>
		public RectTransform Rect {
			get {
				return rect;
			}
			set {
				this.rect = value;
			}
		}

		public RectTransform Background {
			get {
				return background;
			}
			set {
				this.background = value;
			}
		}

		#endregion

		#region Unity Editor

#if UNITY_EDITOR

		private void OnValidate() {

			if (thisRect == null) {
				Start();
			}

			if (width <= 0) {
				width = 1f;
			}

			if (height <= 0) {
				height = 1f;
			}

			if (value > 1f) {
				value = 1f;
			}
			if (value < 0f) {
				value = 0f;
			}

			if (!Application.isPlaying) {
				currentValue = value;
			}

			Update();

		}

#endif

		#endregion

	}

}
