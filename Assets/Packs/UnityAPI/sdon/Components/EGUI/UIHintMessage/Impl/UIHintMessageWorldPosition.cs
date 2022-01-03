using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Всплывающее сообщение/подсказка появляющаяся в мировых координатах
	/// </summary>
	public class UIHintMessageWorldPosition : UIShowedHintMessage {

        public override void OnUpdate()
        {
            base.OnUpdate();
            Rect.position = Camera.main.WorldToScreenPoint(Position) + offset;
        }

        /// <summary>
        /// Устанавливает положение на экране
        /// </summary>
        public override Vector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
#if UNITY_EDITOR
                if(Camera.main == null) // Из-за ExecuteInEditMode такое возможно
                    return;
#endif
                Vector3 pos = Camera.main.WorldToScreenPoint(this.position);
                Rect.position = pos;
            }
        }

    }

}
