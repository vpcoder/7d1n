using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Всплывающее сообщение/подсказка появляющаяся на экране
	/// </summary>
	public class UIHintMessageScreenPosition : UIShowedHintMessage {

		#region Unity Events

		/// <summary>
		/// Продолжает метод Start у MonoBehaviour
		/// </summary>
		public override void OnUpdate() {

			base.OnUpdate();

			Vector3 position = Position;
			position.y = Screen.height - position.y;

			Rect.position = position;

		}

		#endregion

	}

}
