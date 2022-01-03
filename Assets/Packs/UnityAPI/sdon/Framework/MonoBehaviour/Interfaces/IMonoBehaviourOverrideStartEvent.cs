using System;

namespace UnityEngine {

	/// <summary>
	/// Интерфейс продолжающий Start метод у MonoBehaviour
	/// </summary>
	public interface IMonoBehaviourOverrideStartEvent {

        /// <summary>
        /// Продолжает метод Start у MonoBehaviour
        /// </summary>
		void OnStart();

	}

}

