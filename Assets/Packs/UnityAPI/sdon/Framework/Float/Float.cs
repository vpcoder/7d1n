using System;

namespace UnityEngine {

	public static class FloatAdditions {

		/// <summary>
		/// Возвращает точку ограниченную отрезком
		/// </summary>
		/// <param name="value">Исходная точка на отрезке</param>
		/// <param name="min">Точка минимума, лежащая на отрезке</param>
		/// <param name="max">Точка максимума, лежащая на отрезке</param>
		/// <returns></returns>
		public static float InFrame(this float value, float min, float max) {
			if (value < min)
				return min;
			if (value > max)
				return max;
			return value;
		}

	}

}
