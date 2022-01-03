using System;

namespace Engine.EGUI {

	/// <summary>
	/// Определяет привязку UI композитов внутри контейнера
	/// </summary>
	public enum InnerAlignType : int {
	
		/// <summary>Композит привязывается по левому верхнему углу, не растягивается</summary>
		Fixed,

		/// <summary>Композит привязывается по левой середине, растягивается по вертикали</summary>
		FixedHorizontal,

		/// <summary>Композит привязывается верхнему центру, растягивается по горизонтали</summary>
		FixedVertical

	};

}

