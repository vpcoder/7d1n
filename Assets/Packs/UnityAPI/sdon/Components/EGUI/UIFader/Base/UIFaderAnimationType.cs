
namespace Engine.EGUI {

	/// <summary>
	/// Определяет тип анимации
	/// </summary>
	public enum UIFaderAnimationType {

		/// <summary>
		/// Исчезание UI слоя
		/// </summary>
		AlphaAnimation    = 0x00,

		/// <summary>
		/// Уменьшение масштаба UI слоя
		/// </summary>
		ScaleAnimation    = 0x01,

		/// <summary>
		/// Перемещение UI слоя в пространстве
		/// </summary>
		PositionAnimation = 0x02
		
	};

}
