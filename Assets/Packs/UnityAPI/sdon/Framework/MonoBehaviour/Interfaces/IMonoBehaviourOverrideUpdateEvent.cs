using System;

namespace UnityEngine {

	/// <summary>
	/// Интерфейс продолжающий Update метод у MonoBehaviour
	/// </summary>
	public interface IMonoBehaviourOverrideUpdateEvent {

		/// <summary>
		/// Продолжает метод Update у MonoBehaviour
		/// </summary>
		void OnUpdate();

	}

}

