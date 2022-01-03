using UnityEngine;
using System.Collections.Generic;

namespace Engine.Enemy {

	public interface IPather {

		#region Characteristics

		/// <summary>
		/// Утанавливает/Возвращает скорость хотьбы
		/// </summary>
		float SpeedWalk { get; set; }

		/// <summary>
		/// Устанавливает/Возвращает скорость бега
		/// </summary>
		float SpeedRun  { get; set; }

		#endregion

		#region Properties

		/// <summary>
		/// Возвращает/Устанавливает тип передвижения
		/// </summary>
		MoveType MoveType { get; set; }

		/// <summary>
		/// Возвращает/Устанавливает точку, в которую необходимо двигаться AI
		/// </summary>
		Vector3 Point     { get; set; }
		/// <summary>
		/// Возвращает/Устанавливает цель, за которой надо "охотиться" AI
		/// </summary>
		Transform Target  { get; set; }
		/// <summary>
		/// Устанавливает/Возвращает текущий угол обзора AI
		/// </summary>
		Quaternion Look   { get; set; }

		/// <summary>
		/// Возвращает текущий массив точек пути (null, если стоит на месте)
		/// </summary>
		Vector3[]  Path   { get; }

		/// <summary>
		/// Возвращает время, когда был установлен target. Если цели не было установлено, вернёт 0
		/// </summary>
		float TimeTargetSetup { get; }

		/// <summary>
		/// Возвращает время, в течение которого AI приследует цель с момента установки цели. Если цели не было установлено, вернёт 0
		/// </summary>
		float TimeTargetHunted { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Указывает идти в точку point
		/// </summary>
		/// <param name="point">Точка, куда необходимо идти</param>
		void Move(Vector3 point);

		/// <summary>
		/// Меняет тип хотьбы
		/// </summary>
		/// <param name="type">Новый тип хотьбы</param>
		void SetMoveType(MoveType type);

		/// <summary>
		/// Указывает цель для преследования (устанавливает "охоту")
		/// </summary>
		/// <param name="target">Цель преследования</param>
		void Haunt(Transform target);

		/// <summary>
		/// Возвращает логическое значение - преследует ли текущий AI цель или нет
		/// </summary>
		/// <returns>true - если target задан, и AI преследует цель</returns>
		bool isHaunted();

		#endregion

	}

}
