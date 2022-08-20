using UnityEngine;
using UnityEngine.UI;

namespace Engine.EGUI {

	/// <summary>
	/// Класс-шина для всплывающих сообщений/подсказок
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(VerticalLayoutGroup))]
	[RequireComponent(typeof(ContentSizeFitter))]
	public abstract class UIHintMessage : MonoBehaviour, IHintMessage {

        #region Shared Fields

        [SerializeField] protected bool useMoveDirection = false;
        [SerializeField] protected float moveDirectionSpeed = 1f;

		[Caption("Текстовое поле")]
		[Comments("Указывает ссылку на компонент текстового поля, в котором будет отображаться текст")]
		[SerializeField] protected Text textField;

		[Caption("Время жизни")]
		[Comments("Время в секундах, в течение которого будет жить компонент")]
		[SerializeField] protected float delay = 2f;

		[Caption("Координаты")]
		[Comments("Положение сообщения на экране")]
		[SerializeField] protected Vector3 position;

		[Caption("Размер")]
		[Comments("Размер окна сообщения")]
		[SerializeField] protected Vector2 size;

		[Caption("Рамка")]
		[Comments("Отступы от границы фона сообщения до текста")]
		[SerializeField] protected Border border = new Border(0, 0, 0, 0);

		#endregion

		#region Hide Fields

		private float timestamp;

		private RectTransform textFieldRect;

		private RectTransform rect;

		private bool destroyFlag = false;

		private bool destroyEventFlag = false;
		
		#endregion

		/// <summary>
		/// Вызывается в момент, когда необходимо уничтожить сообщение
		/// </summary>
		public abstract void OnDestroyEvent();

		/// <summary>
		/// Продолжает метод Start у MonoBehaviour
		/// </summary>
		public virtual void OnStart()  { }

		/// <summary>
		/// Продолжает метод Update у MonoBehaviour
		/// </summary>
		public virtual void OnUpdate() { }

		private void Start() {
			timestamp = Time.time;
			textFieldRect = textField.GetComponent<RectTransform>();
			OnStart();
		}

		private void Update() {
			
			OnUpdate();

#if UNITY_EDITOR

			if (!Application.isPlaying) {
				return;
			}

#endif

			if (Time.time - timestamp < delay) {
				return;
			}

			if (!destroyEventFlag) {
				OnDestroyEvent();
				destroyEventFlag = true;
			}

			if (isDestroy()) {
				GameObject.Destroy(transform.gameObject);
			}

		}

		/// <summary>
		/// Возвращает true, если сообщение было помечено как уничтожено
		/// </summary>
		/// <returns></returns>
		public bool isDestroy() {
			return this.destroyFlag;
		}

		public bool isDestroyEvent() {
			return this.destroyEventFlag;
		}

		/// <summary>
		/// Устанавливает флаг необходимости уничтожить объект сообщения
		/// </summary>
		/// <param name="destroyFlag">Значение флага</param>
		public void SetDestroy(bool destroyFlag){
			this.destroyFlag = destroyFlag;
		}

		#region Properties
		
		/// <summary>
		/// Устанавливает время жизни сообщения
		/// </summary>
		public float Delay {
			get {
				return this.delay;
			}
			set {
				this.delay = value;
			}
		}

		/// <summary>
		/// Устанавливает размер окна сообщения
		/// </summary>
		public Vector2 Size {
			get {
				return this.size;
			}
			set {
				this.size = value;
				Rect.sizeDelta = new Vector2(size.x, Rect.sizeDelta.y);
			}
		}

		/// <summary>
		/// Устанавливает положение на экране
		/// </summary>
		public virtual Vector3 Position {
			get {
				return this.position;
			}
			set {
				this.position = value;
            }
		}

		/// <summary>
		/// Устанавливает текст сообщения
		/// </summary>
		public string Text {
			get {

#if UNITY_EDITOR
				if (this.textField == null) {
					Debug.LogError("Не указана ссылка на текстовое поле!");
					throw new MissingReferenceException();
				}
#endif

				return this.textField.text;

			}
			set {

#if UNITY_EDITOR
				if (this.textField == null) {
					Debug.LogError("Не указана ссылка на текстовое поле!");
					throw new MissingReferenceException();
				}
#endif

				this.textField.text = value;

			}
		}

		/// <summary>
		/// Возвращает ссылку на RectTransform текстового поля
		/// </summary>
		public RectTransform TextFieldRect {
			get {
				return textFieldRect;
			}
		}

		/// <summary>
		/// Отступы границ виджета
		/// </summary>
		/// <value>Границы</value>
		public IBorder Border {
			get {
				return border;
			}
			set {
				this.border = value as Border;
				VerticalLayoutGroup group = GetComponent<VerticalLayoutGroup>();
				group.padding.left   = (int)border.Left;
				group.padding.right  = (int)border.Right;
				group.padding.top    = (int)border.Top;
				group.padding.bottom = (int)border.Bottom;
			}
		}

		/// <summary>
		/// RectTransform виджета
		/// </summary>
		/// <value>Возвращает объект RectTransform виджета</value>
		public RectTransform Rect {
			get {
				return GetComponent<RectTransform>();
			}
		}

		#endregion

		#region Editor

		private void OnValidate() {

			Border = border;
			Position = position;
			Size = size;

			if (textField != null) {
				Text = Text;
			}

		}

		#endregion

	}

}
