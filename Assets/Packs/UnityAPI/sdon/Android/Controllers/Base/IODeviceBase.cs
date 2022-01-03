using System;
using UnityEngine;

namespace Engine.IO
{

	/// <summary>
	/// Базовый класс устройства ввода
	/// </summary>
	public abstract class IODeviceBase : MonoBehaviour, IInputDevice, IMonoBehaviourOverrideStartEvent
	{

		#region Hidden Fields

		/// <summary>
		/// текущее состояние ввода
		/// </summary>
		protected Vector3 inputVector;

		#endregion

		private void Start()
		{
			OnStart();
		}

		/// <summary>
		/// Продолжает метод Start у MonoBehaviour
		/// </summary>
		public virtual void OnStart()
		{
		}

		#region Events

		/// <summary>
		/// Вызывается каждый раз когда значение inputVector меняется
		/// </summary>
		public abstract void OnChangeListener();

		#endregion

		#region Properties

		/// <summary>
		/// Возвращает текущее состояние ввода
		/// </summary>
		public Vector3 InputVector
		{
			get
			{
				return inputVector;
			}
		}

		#endregion

	}

}
