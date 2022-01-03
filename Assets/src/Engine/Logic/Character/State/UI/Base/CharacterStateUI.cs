using UnityEngine;
using UnityEngine.UI;

namespace Engine.Character
{

    /// <summary>
    /// Базовый класс для параметра состояния
    /// </summary>
	public abstract class CharacterStateUI : MonoBehaviour, IMonoBehaviourOverrideUpdateEvent, IMonoBehaviourOverrideStartEvent
	{

        #pragma warning disable IDE0044, IDE0051

        [SerializeField]
		private Text field;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private int value;

		[SerializeField]
		private Color minColor;

		[SerializeField]
		private Color maxColor;

        /// <summary>
        /// Текущее значение параметра в процентах от 0 до 100
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

		private void Start()
		{
			OnStart();
		}

		private void Update()
		{
			OnUpdate();
			
			var color = Color.LerpUnclamped(minColor, maxColor, (float)value / 100f);
			icon.color = color;

			field.text = value + "%";
		}

		public virtual void OnStart() { }

		public virtual void OnUpdate() { }

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

    }

}
