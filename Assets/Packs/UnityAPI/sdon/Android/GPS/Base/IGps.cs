using UnityEngine;

namespace Engine.IO
{

	/// <summary>
	/// Интерфейс получения данных с GPS
	/// </summary>
	public interface IGps
	{

		#region Fields

        Vector3 DebugPosition { get; set; }

        /// <summary>
        /// Возвращает предыдущую позицию GPS
        /// </summary>
        Vector3 LastPosition
		{
			get;
		}

		/// <summary>
		/// Возвращает текущую позицию GPS
		/// </summary>
		Vector3 CurrentPosition
		{
			get;
		}

		/// <summary>
		/// Текущий горизонтальный угол
		/// </summary>
		float CurrentHAccuracy
		{
			get;
		}

		/// <summary>
		/// Предыдущий горизонтальный угол
		/// </summary>
		float LastHAccuracy
		{
			get;
		}

		/// <summary>
		/// Текущий горизонтальный угол
		/// </summary>
		float CurrentVAccuracy
		{
			get;
		}

		/// <summary>
		/// Предыдущий горизонтальный угол
		/// </summary>
		float LastVAccuracy
		{
			get;
		}

		/// <summary>
		/// Возвращает логическое значение: "Включён ли GPS на устройстве?"
		/// </summary>
		bool IsEnabled
		{
			get;
		}

		/// <summary>
		/// Возвращает логическое значение: "Запущено ли GPS отслеживание?"
		/// </summary>
		bool IsStarted
		{
			get;
		}

		/// <summary>
		/// Интервал времени между началом получения текущей координаты GPS и текущим временем.
		/// (Показывает, сколько секунд прошло с момента последнего опроса текущей координаты)
		/// </summary>
		float LastUpdateTime
		{
			get;
		}

		/// <summary>
		/// Текущее состояние GPS навигации
		/// </summary>
		GpsStatusType Status
		{
			get;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Запускает работу GPS
		/// </summary>
		void StartGps();

		/// <summary>
		/// Останавливает работу GPS
		/// </summary>
		void StopGps();

        /// <summary>
        /// Принудительно присваевает текущую позицию
        /// </summary>
        /// <param name="pos">Текщая позиция</param>
        void SetPos(Vector3 pos);

		#endregion

	}

}
