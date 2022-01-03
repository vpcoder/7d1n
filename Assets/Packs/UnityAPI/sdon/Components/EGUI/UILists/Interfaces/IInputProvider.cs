using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс определяющий входные в IViewer данные
	/// </summary>
	public interface IInputProvider : IMonoBehaviourOverrideStartEvent, IMonoBehaviourOverrideUpdateEvent {

		/// <summary>
		/// Метод срабатывает в момент, когда получены новые входные данные и необходимо перестроить UI композиты на экране компонента 
		/// </summary>
		void OnContent();

		/// <summary>
		/// Устанавливает/Возвращает входные данные для проводника
		/// </summary>
		/// <param name="input">Входные данные</param>
		object Input {
			get;
		}

		/// <summary>
		/// Устанавливает входные данные для проводника
		/// </summary>
		/// <param name="input">Входные данные</param>
		/// <param name="forceUpdate">Устанавливает флаг обязательного обновления контента. Если true - контент принудительно пересоздаётся</param>
		void SetInput(object input, bool forceUpdate);

	}

}

