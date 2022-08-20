using System;

namespace UnityEditor {

	/// <summary>
	/// Интерфейс продолжающий Start метод у Editor
	/// </summary>
	public interface IEditorStartEvent {

		void OnStart();

	}

}

