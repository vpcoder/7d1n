using UnityEngine;
using System.Collections;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс для всплывающего сообщения/подсказки
	/// </summary>
	public interface IHintMessage : IWidget, IMonoBehaviourOverrideStartEvent, IMonoBehaviourOverrideUpdateEvent {

		/// <summary>
		/// Устанавливает время жизни сообщения
		/// </summary>
		float Delay {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает текст сообщения
		/// </summary>
		string Text {
			get;
			set;
		}

		/// <summary>
		/// Устанавливает положение сообщения на экране
		/// </summary>
		Vector3 Position {
			set;
			get;
		}

		/// <summary>
		/// Устанавливает размер окна сообщения
		/// </summary>
		Vector2 Size {
			set;
			get;
		}

		/// <summary>
		/// Вызывается в момент, когда необходимо уничтожить сообщение
		/// </summary>
		void OnDestroyEvent();

		/// <summary>
		/// Возвращает true, если сообщение помечено как уничтоженное
		/// </summary>
		/// <returns></returns>
		bool isDestroy();

		/// <summary>
		/// Устанавливает флаг необходимости уничтожить объект сообщения
		/// </summary>
		/// <param name="destroyFlag">Значение флага</param>
		void SetDestroy(bool destroyFlag);
		
	}

}
