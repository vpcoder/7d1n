using UnityEngine;
using UnityEngine.UI;
using Engine;

namespace Engine.EGUI {

	/// <summary>
	/// Класс-шина для анимации UI панелей
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Graphic))]
	public abstract class UIFader : MonoBehaviour, IFader {

		#region Shared Fields

		[Caption("Выключает компоненты")]
		[Comments("Выключает дочерние tranfsorm компоненты когда UI панель считается скрытой (включает, когда панель считается видимой)")]
		[SerializeField] protected bool disableChilds = true;

		[Caption("Перерисовывать каскадно")]
		[Comments("Поле показывает, нужно ли применять перерисовку OnDrawEvent каскадно (всем дочерним элементам текущего UI слоя.\nFader с fadeCascade=true в несколько раз медленее, однако, позволяет сделать более эстетичную анимацию")]
		[SerializeField] protected bool fadeCascade = false;

		[Caption("Переключатель")]
		[Comments("Задаёт групповой переключатель. Если null, любые скрытия и отображения касаются только текущей панели. Если есть переключатель, при отображении текущей панели, скрываются остальные панели в группе")]
		[SerializeField] protected IFaderSwitcher switcher = null;

		[Caption("Максимальная видимость")]
		[Comments("Показывает максимально допустимое значение видимости панели")]
		[SerializeField] [Range(0f,1f)] protected float maxVisible = 1f;

		[Caption("Минимальная видимость")]
		[Comments("Показывает минимально допустимое значение видимости панели")]
		[SerializeField] [Range(0f,1f)] protected float minVisible = 0f;

		[Caption("Видимость")]
		[Comments("Показывает установленное значение видимости панели")]
		[SerializeField] [Range(0f,1f)] protected float visible = 0f;

		[Caption("Скорость сокрытия")]
		[Comments("Показывает скорость приближения текущего значения видимости к выбранному")]
		[SerializeField] protected float fadeSpeed = 10f;

		[Caption("Появляться")]
		[Comments("Меняет стартовое состояние фейдера, когда выбран, фейдер появляется, когда не выбран, фейдер пропадает")]
		[SerializeField] protected bool hideOnStart = true;

		#endregion 


		#region Hide Fields

		/// <summary>
		/// Показывает текущее переходное значение видимости панели
		/// </summary>
		private float currentState;

		/// <summary>
		/// Хранит ссылку на RectTransform объект
		/// </summary>
		private RectTransform rect = null;

		/// <summary>
		/// Хранит ссылку на Graphic объект
		/// </summary>
		private Graphic graphic = null;

		/// <summary>
		/// Показывает минимальный порог разности между значениями (чувствительности)
		/// </summary>
		private const float SENSETIVITY_STEP = 0.0005f;

		/// <summary>
		/// Минимальное значение Visible
		/// </summary>
		private const float MIN = 0f;

		/// <summary>
		/// Максимальное значение Visible
		/// </summary>
		private const float MAX = 1f;

		/// <summary>
		/// Время начала анимации
		/// </summary>
		private float startTime;

		private bool visibleFlag;

		#endregion


		#region Unity Events

		private void Start() {

#if UNITY_EDITOR

			if (!Application.isPlaying) {
				currentState = visible;
			}

#endif

			rect         = GetComponent<RectTransform>();
			graphic      = GetComponent<Graphic>();
			OnStart();

#if UNITY_EDITOR

			OnValidate();

#endif

		}

		private void Update() {

			if (Mathf.Abs(visible-currentState)<=SENSETIVITY_STEP) {
				return;
			}
			
			currentState = Mathf.Lerp(currentState, visible, (Time.time - startTime) * fadeSpeed);

			if (currentState > MIN) {
				visibleFlag = true;
			}

			if (graphic == null) {
				return;
			}

			OnDrawEvent(rect, graphic, currentState, false);

			if (fadeCascade) {
				setCascadeRedraw(transform);
			}

			if (visible == MAX && Mathf.Abs(MAX - currentState) <= SENSETIVITY_STEP) {
				currentState = MAX;
				OnFullShowEvent();
			}

			if (visible == MIN && Mathf.Abs(currentState) <= SENSETIVITY_STEP) {
				visibleFlag = false;
				currentState = MIN;
				OnFullHideEvent();
			}

		}

		/// <summary>
		/// Каскадно вызывает перерисовку для всех дочерних элементов UI слоя
		/// </summary>
		/// <param name="transform">UI слой, с которого будут перерисованны все дочерние элементы</param>
		private void setCascadeRedraw(Transform transform) {

			foreach (Transform child in transform.Childs()) {

				RectTransform rect    = child.GetComponent<RectTransform>();
				Graphic       graphic = child.GetComponent<Graphic>();

				if (rect != null && graphic != null) {
					OnDrawEvent(rect, graphic, currentState, true);
				}

				if (child.childCount > 0) {
					setCascadeRedraw(child);
				}

			}

		}

		#endregion


		#region Fader Events

		/// <summary>
		/// Возвращает состояние - виден ли элемент или нет
		/// </summary>
		public bool isVisible() {
			return visibleFlag || currentState > MIN || visible > MIN;
		}

		/// <summary>
		/// Устанавливает начальное значение видимости как "скрыто"
		/// </summary>
		public void SetupHide() {
			currentState = MIN;
			Visible = MAX;
			visibleFlag = true;
		}

		/// <summary>
		/// Устанавливает начальное значение видимости как "показывать"
		/// </summary>
		public void SetupShow() {
			currentState = MAX;
			Visible = MIN;
			visibleFlag = true;
		}

		/// <summary>
		/// Перерисовывает UI слой относительно заданной альфы (**Вызывается только в том случае, когда WidgetGraphic не равен null**, тоесть, this.GetComponent<Graphic>()!=null)
		/// </summary>
		/// <param name="rect">RectTransform объект, для которого совершается перерисовка анимации</param>
		/// <param name="graphic">Graphic объект, для которого совершается перерисовка анимации</param>
		/// <param name="visible">Значение видимости от 0f до 1f</param>
		/// <param name="isChild">Флаг показывает, является ли текущий RectTransform дочерним (true) или родительским (false)</param>
		public virtual void OnDrawEvent(RectTransform rect, Graphic graphic, float visible, bool isChild) { }

		/// <summary>
		/// Срабатывает когда UI слой полностью скрылся
		/// </summary>
		public virtual void OnFullHideEvent() {

			if (!disableChilds) {
				return;
			}

			foreach (Transform child in transform.Childs()) {
				child.gameObject.SetActive(false);
			}

		}

		/// <summary>
		/// Устанавливает/Возвращает переключатель для текущего слоя
		/// </summary>
		public IFaderSwitcher Switcher {
			get {
				return this.switcher;
			}
			set {
				this.switcher = value;
			}
		}

		/// <summary>
		/// Срабатывает когда UI слой полностью отобразился
		/// </summary>
		public virtual void OnFullShowEvent() {

			if (!disableChilds || fadeCascade) {
				return;
			}

			foreach (Transform child in transform.Childs()) {
				child.gameObject.SetActive(true);
			}

		}

		/// <summary>
		/// Продолжает Start метод у MonoBehaviour
		/// </summary>
		public virtual void OnStart() {
			if (this.hideOnStart) {
				SetupHide();
			} else {
				SetupShow();
			}
		}

		/// <summary>
		/// Отображает UI элемент
		/// </summary>
		public void OnShow() {

			if (switcher != null) {
				switcher.OnHide(this);
			}

			visibleFlag = true;
			Visible = MAX;

			transform.SetAsLastSibling();

		}

		/// <summary>
		/// Скрывает UI элемент
		/// </summary>
		public void OnHide() {
			Visible = MIN;

			if (!fadeCascade && disableChilds) {
				foreach (Transform child in transform.Childs()) {
					child.gameObject.SetActive(false);
				}
			}

		}

		/// <summary>
		/// Изменяет видимость элемента
		/// </summary>
		/// <param name="visible">Значение видимости от 0f до 1f.</param>
		public float Visible {
			get {
				return visible;
			}
			set {

				if (value == visible) {
					return;
				}

				startTime = Time.time;

				if (value <= MIN) {
					this.visible = MIN;
					return;
				}

				if (disableChilds) {
					if (fadeCascade) {
						foreach (Transform child in transform.Childs()) {
							child.gameObject.SetActive(true);
						}
					}
				}

				if (value >= MAX) {
					this.visible = MAX;
					return;
				}

				this.visible = value;

			}
		}

		/// <summary>
		/// Возвращает/Устанавливает максимальную границу видимости
		/// </summary>
		public float VisibleMax {
			get {
				return maxVisible;
			}
			set {
				if (value > 1f) {
					maxVisible = 1f;
					return;
				}
				if(value< 0.001f) {
					maxVisible = 0.001f;
					return;
				}
				maxVisible = value;
			}
		}

		/// <summary>
		/// Возвращает/Устанавливает минимальную границу видимости
		/// </summary>
		public float VisibleMin {
			get {
				return minVisible;
			}
			set {
				if (value < 0f) {
					minVisible = 0f;
					return;
				}
				if (value > 0.999f) {
					minVisible = 0.999f;
					return;
				}
				minVisible = value;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает параметр "выключать дочерние transform компоненты".
		/// </summary>
		public bool DisableChilds {
			get {
				return disableChilds;
			}
			set {
				disableChilds = value;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает параметр "делать OnDrawEvent каскадно (для всех дочерних элементов текущего UI слоя)"
		/// </summary>
		public bool FadeCascade {
			get {
				return fadeCascade;
			}
			set {
				fadeCascade = value;
			}
		}

		/// <summary>
		/// Graphic виджета
		/// </summary>
		/// <value>The graphic.</value>
		public virtual Graphic WidgetGraphic {
			get {
				return graphic;
			}
		}

		#endregion


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
		public virtual RectTransform Rect {
			get {
				return rect;
			}
		}

		#endregion


		#region Unity Editor

#if UNITY_EDITOR

		private void OnValidate() {

			if (maxVisible>1f) {
				maxVisible = 1f;
			}
			if (maxVisible <= 0f) {
				maxVisible = 0.001f;
			}
			if (minVisible >= 1f) {
				minVisible = 0.999f;
			}
			if (minVisible < 0f) {
				minVisible = 0f;
			}

			if (visible > 0f && fadeCascade && disableChilds) {
				foreach (Transform child in transform.Childs()) {
					child.gameObject.SetActive(true);
				}
			}

			if (visible == 0f && disableChilds) {
				foreach (Transform child in transform.Childs()) {
					child.gameObject.SetActive(false);
				}
			}

			if (graphic != null) {
				OnDrawEvent(rect,graphic,visible,false);
				if (fadeCascade) {
					setCascadeRedraw(transform);
				}
			}

			if (!Application.isPlaying) {
				currentState = visible;
			}

		}

#endif

		#endregion

	}

}
