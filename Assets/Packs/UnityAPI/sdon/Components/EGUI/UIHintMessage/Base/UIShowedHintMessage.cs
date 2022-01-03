using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Прослойка для UIHintMessage, осуществляющая сокрытие и появление сообщения с подсказкой
	/// </summary>
	[RequireComponent(typeof(UIFader))]
	public abstract class UIShowedHintMessage : UIHintMessage {

		#region Shared Fields

		private UIFader fader;

        private Vector3 direction;

        protected Vector3 offset;

		#endregion

		#region Unity Events

		/// <summary>
		/// Продолжает метод Start у MonoBehaviour
		/// </summary>
		public override void OnStart() {
			fader = GetComponent<UIFader>();
			fader.OnShow();

            direction = UnityEngine.Random.onUnitSphere;
            direction.z = 0;
            direction.y = Mathf.Abs(direction.y);
            direction *= moveDirectionSpeed;
        }

		/// <summary>
		/// Продолжает метод Start у MonoBehaviour
		/// </summary>
		public override void OnUpdate() {

#if UNITY_EDITOR

			if (!Application.isPlaying) {
				return;
			}

#endif
            if (useMoveDirection)
            {
                offset += direction;
            }

            if (fader.isVisible()) {
				return;
			}

			if (!isDestroy() && isDestroyEvent()) {
				SetDestroy(true);
			}

		}

		#endregion

		#region Events

		/// <summary>
		/// Вызывается в момент, когда необходимо уничтожить сообщение
		/// </summary>
		public override void OnDestroyEvent() {
			fader.OnHide();
		}

		#endregion

	}

}
