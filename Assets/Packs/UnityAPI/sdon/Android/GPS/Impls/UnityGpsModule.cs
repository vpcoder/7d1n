using System;
using UnityEngine;
using System.Collections;

namespace Engine.IO
{

	/// <summary>
	/// Реализация базового класса IOGpsBase получения GPS-данных на основе Unity - Input.location
	/// </summary>
	public class UnityGpsModule : IOGpsBase
	{

		private float lastTime = 0f;

		public override bool IsEnabled
		{
			get
			{
#if UNITY_EDITOR && DEBUG
                return true;
#else
				isEnabled = Input.location.isEnabledByUser;
				return isEnabled;
#endif
			}
		}

		public override void StopGps()
		{
			base.StopGps();
			Input.location.Stop();
		}

		/// <summary>
		/// Инициализирует GPS
		/// </summary>
		protected override void DoStartGpsCoroutine()
		{
			if (Time.time - lastTime < updateTime)
				return;

			lastTime = Time.time;

#if UNITY_EDITOR && DEBUG
            status = GpsStatusType.Running;
#else
            status = Input.location.status.ToGpsStatusType();
#endif

			if (LastUpdateTime > maxTimeout)
				status = GpsStatusType.Failed;

			if (status != GpsStatusType.Running && status != GpsStatusType.Initializing)
			{
				Input.location.Start();
				return;
			}

			if (!needStarted || status != GpsStatusType.Running)
				return;

			startedFlag = true;
			needStarted = false;
		}

		/// <summary>
		/// Обновляет данные полученные от GPS
		/// </summary>
		protected override void DoUpdateGpsCoroutine()
		{
			if (Time.time - lastTime < updateTime)
				return;

			lastTime = Time.time;

			// Обновляем сведения о статусе
			status = Input.location.status.ToGpsStatusType();

			if (LastUpdateTime > maxTimeout)
				status = GpsStatusType.Failed;

			if (!isEnabled || status != GpsStatusType.Running)
				return; // Что-то пошло не так, возможно, кто то выключил или остановил GPS 

			lastPosition = currentPosition;

            // Обновляем состояния
            currentPosition = new Vector3(
				Input.location.lastData.latitude,
				Input.location.lastData.longitude,
				Input.location.lastData.altitude
			);

            // Обновляем угол
            lastHAccuracy = currentHAccuracy;
			lastVAccuracy = currentVAccuracy;

			currentHAccuracy = Input.location.lastData.horizontalAccuracy;
			currentVAccuracy = Input.location.lastData.verticalAccuracy;

			this.lastUpdateTime = Time.time;
		}

	}

}
