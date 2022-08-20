
namespace UnityEditor {

	/// <summary>
	/// Тип видимости редактора по умолчанию.
	/// </summary>
	public enum EditorVisibleType {

		/// <summary>
		/// Редактор "по умолчанию" скрыт во вкладке "Редактор"
		/// </summary>
		HideInFader = 0x00,
		/// <summary>
		/// Редактор "по умолчанию" всегда виден
		/// </summary>
		Show        = 0x01,
		/// <summary>
		/// Редактор "по умолчанию" никогда не виден
		/// </summary>
		FullHide    = 0x02

	};

}
