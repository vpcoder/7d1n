using UnityEngine;
using UnityEngine.UI;

namespace Engine.Character
{

    /// <summary>
    /// 
    /// Базовый класс для параметра состояния
    /// ---
    /// Base class for the state parameter
    /// 
    /// </summary>
	public abstract class CharacterStateUI : MonoBehaviour,
                                             IMonoBehaviourOverrideUpdateEvent,
                                             IMonoBehaviourOverrideStartEvent
	{

#pragma warning disable IDE0044, IDE0051

        /// <summary>
        ///     Время ожидания между обновлениями состояния
        ///     ---
        ///     Waiting time between status updates
        /// </summary>
        private const float UPDATE_SPEED = 0.2f; // 200млс.

        #region Hidden Fields

        /// <summary>
        ///     Ссылка на объект текста, в котором будут показаны проценты параметра
        ///     ---
        ///     Reference to the text object that will show the percentage of the parameter
        /// </summary>
        [SerializeField]
		private Text field;

        /// <summary>
        ///     Ссылка на объект иконки параметра. Иконка будет подкрашиваться от minColor до maxColor в зависимости от value
        ///     ---
        ///     Reference to the icon object of the parameter. The icon will be tinted from minColor to maxColor depending on the value
        /// </summary>
		[SerializeField]
		private Image icon;

        /// <summary>
        ///     Текущее значение параметра в процентах от 0 до 100
        ///     ---
        ///     The current value of the parameter in percent from 0 to 100
        /// </summary>
		[SerializeField]
		private int value;

        /// <summary>
        ///     Конечный цвет иконки к значению 0%
        ///     ---
        ///     The final color of the icon to the 0% value
        /// </summary>
        [SerializeField]
		private Color minColor;

        /// <summary>
        ///     Начальный цвет иконки, от значения в 100%
        ///     ---
        ///     
        ///     Initial icon color, from a value of 100%
        /// </summary>
        [SerializeField]
		private Color maxColor;


        /// <summary>
        ///     Время последнего обновления состояния
        ///     ---
        ///     Time of last status update
        /// </summary>
        private float lastTimestamp;

        #endregion

        #region Properties

        /// <summary>
        ///     Текущее значение параметра в процентах от 0 до 100
        ///     ---
        ///     The current value of the parameter in percent from 0 to 100
        /// </summary>
        public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;

				if (this.value < 0)
					this.value = 0;

				if (this.value > 100)
					this.value = 100;
			}
		}

        #endregion

        #region Methods

        public virtual void OnStart() { }

        public virtual void OnUpdate() { }

        #endregion

        #region Unity Events

        private void Start()
		{
			OnStart();
        }

		private void Update()
		{
            if (Time.time - lastTimestamp < UPDATE_SPEED)
                return;

            lastTimestamp = Time.time;

			OnUpdate();
			
			var color = Color.LerpUnclamped(minColor, maxColor, (float)value / 100f);
			icon.color = color;

			field.text = value + "%";
		}

#if UNITY_EDITOR && DEBUG

        void OnValidate()
		{
			if (this.value < 0)
				this.value = 0;

			if (this.value > 100)
				this.value = 100;

			Update();
		}

#endif

        #endregion


    }

}
