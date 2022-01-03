using System;

namespace UnityEngine {

	/// <summary>
	/// Перечисление пресетов выравнивания rectTransform компонента
	/// </summary>
	public enum AnchorPresets : int {

		/// <summary>
		/// Выравнивание по левому верхнему углу
		/// </summary>
		LeftTop      = 0x00,

		/// <summary>
		/// Выравнивание по середине сверху
		/// </summary>
		MiddleTop    = 0x01,

		/// <summary>
		/// Выравнивание по правму верхнему углу
		/// </summary>
		RightTop     = 0x02,

		/// <summary>
		/// Выравнивание слева по середине
		/// </summary>
		LeftCenter   = 0x03,

		/// <summary>
		/// Выравнивание по центру экрана
		/// </summary>
		MiddleCenter = 0x04,

		/// <summary>
		/// Выравнивание по правому центру
		/// </summary>
		RightCenter  = 0x05,
		
		/// <summary>
		/// Выравнивание по  левому нижнему углу
		/// </summary>
		LeftBottom   = 0x06,

		/// <summary>
		/// Выравнивание по центру снизу
		/// </summary>
		MiddleBottom = 0x07,

		/// <summary>
		/// Выравнивание по правому нижнему углу
		/// </summary>
		RightBottom  = 0x08,

		
		/// <summary>
		/// Заполняет всё пространство по X, выравнивается сверху по Y
		/// </summary>
		FillTop      =-0x01,

		/// <summary>
		/// Заполняет всё пространство по X, выравнивается по середине по Y
		/// </summary>
		FillCenter   =-0x02,

		/// <summary>
		/// Заполняет всё пространство по X, выравнивается снизу по Y
		/// </summary>
		FillBottom   =-0x03,

		/// <summary>
		/// Выравнивается слева по X, заполняет всё пространство по Y
		/// </summary>
		LeftFill     =-0x04,

		/// <summary>
		/// Выравнивается по центру по X, заполняет всё пространство по Y
		/// </summary>
		MiddleFill   =-0x05,

		/// <summary>
		/// Выравнивается справа по X, заполняет всё пространство по Y
		/// </summary>
		RightFill    =-0x06,

		/// <summary>
		/// Заполняет весь экран по X и по Y
		/// </summary>
		FillFull     =-0x07

	};

}
