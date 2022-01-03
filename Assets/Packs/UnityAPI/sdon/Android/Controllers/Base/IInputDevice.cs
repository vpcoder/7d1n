using System;
using UnityEngine;

namespace Engine.IO
{

	/// <summary>
	/// Интерфейс устройства ввода
	/// </summary>
	public interface IInputDevice
	{

		/// <summary>
		/// Возвращает текущее состояние ввода, меняется на отрезке [-1, 1] для каждой оси
		/// </summary>
		Vector3 InputVector
		{
			get;
		}

	}

}
