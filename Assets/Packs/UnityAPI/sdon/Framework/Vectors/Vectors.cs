using System;
using System.Collections.Generic;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **Vectors** для Vector2/Vector3/Vector4 объектов Unity 
    /// Авторы: sdon
    /// Дата: 23.03.2017
    /// Версия: 1.0.00
    /// </summary>
    public static class Vectors {

		private static string[] normalize(string value) {

			if (value.StartsWith("(")) {
				value = value.Substring(1, value.Length - 1);
			}
			if (value.EndsWith(")")) {
				value = value.Substring(0, value.Length - 1);
			}

			return value.Split(',');

		}

		/// <summary>
		/// Возвращает вектор ограниченный прямоугольником
		/// </summary>
		/// <param name="point">Исходная точка в прямоугольнике</param>
		/// <param name="min">Точка минимума, лежащая на прямоугольнике</param>
		/// <param name="max">Точка максимума, лежащая на прямоугольнике</param>
		/// <returns></returns>
		public static Vector2 ToFrame(this Vector2 point, Vector2 min, Vector2 max) {
			return new Vector2(
				point.x.InFrame(min.x, max.x),
				point.y.InFrame(min.y, max.y)
			);
		}

		/// <summary>
		/// Возвращает точные значение вектора в виде строки {0.0; 0.0; 0.0}
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static string ToFltString(this Vector3 vector) {
			return "{" + vector.x.ToString("R") + "; " + vector.y.ToString("R") + "; " + vector.z.ToString("R") + "}";
		}

		/// <summary>
		/// Возвращает вектор ограниченный прямоугольным параллелепипедом
		/// </summary>
		/// <param name="point">Исходная точка в параллелепипеде</param>
		/// <param name="min">Точка минимума, лежащая на пареллепипеде</param>
		/// <param name="max">Точка максимума, лежащая на пареллепипеде</param>
		/// <returns></returns>
		public static Vector3 ToFrame(this Vector3 point, Vector3 min, Vector3 max) {
			return new Vector3(
				point.x.InFrame(min.x, max.x),
				point.y.InFrame(min.y, max.y),
				point.z.InFrame(min.z, max.z)
			);
		}

		/// <summary>
		/// Возвращает вектор ограниченный тессерактом
		/// </summary>
		/// <param name="point">Исходная точка в тессеракте</param>
		/// <param name="min">Точка минимума, лежащая на тессеракте</param>
		/// <param name="max">Точка максимума, лежащая на тессеракте</param>
		/// <returns></returns>
		public static Vector4 ToFrame(this Vector4 point, Vector4 min, Vector4 max) {
			return new Vector4(
				point.x.InFrame(min.x, max.x),
				point.y.InFrame(min.y, max.y),
				point.z.InFrame(min.z, max.z),
				point.w.InFrame(min.w, max.w)
			);
		}

		/// <summary>
		/// Исходный вектор v1 умножается на вектор коэффициентов для каждой соответствующей координаты.
		/// </summary>
		/// <param name="value">Вектор коэффициентов</param>
		/// <returns>Результирующий вектор становится равным {v1.x*v2.x, v1.y*v2.y}</returns>
		public static Vector2 Mul(this Vector2 vector, Vector2 value) {
			return new Vector2(
				vector.x * value.x,
				vector.y * value.y
			);
		}

		/// <summary>
		/// Исходный вектор v1 умножается на вектор коэффициентов для каждой соответствующей координаты.
		/// </summary>
		/// <param name="value">Вектор коэффициентов</param>
		/// <returns>Результирующий вектор становится равным {v1.x*v2.x, v1.y*v2.y, v1.z*v2.z}</returns>
		public static Vector3 Mul(this Vector3 vector, Vector3 value) {
			return new Vector3(
				vector.x * value.x,
				vector.y * value.y,
				vector.z * value.z
			);
		}

		/// <summary>
		/// Исходный вектор v1 умножается на вектор коэффициентов для каждой соответствующей координаты.
		/// </summary>
		/// <param name="value">Вектор коэффициентов</param>
		/// <returns>Результирующий вектор становится равным {v1.x*v2.x, v1.y*v2.y, v1.z*v2.z, v1.w*v2.w}</returns>
		public static Vector4 Mul(this Vector4 vector, Vector4 value) {
			return new Vector4(
				vector.x * value.x,
				vector.y * value.y,
				vector.z * value.z,
				vector.w * value.w
			);
		}

		public static Vector2 ToVector2(this Vector3 vector) {
			return new Vector3(vector.x, vector.y);
		}

		public static Vector2 ToVector2(this Vector4 vector) {
			return new Vector3(vector.x, vector.y);
		}

		public static Vector3 ToVector3(this Vector2 vector) {
			return new Vector3(vector.x, vector.y);
		}

		public static Vector3 ToVector4(this Vector2 vector) {
			return new Vector3(vector.x, vector.y);
		}

		public static Vector3 ToVector4(this Vector3 vector) {
			return new Vector3(vector.x, vector.y, vector.z);
		}

		public static Vector2 Parse(this Vector2 vector, string value) {
			string[] values = normalize(value);
			return new Vector2(float.Parse(values[0]),
							   float.Parse(values[1]));
		}

		public static Vector3 Parse(this Vector3 vector, string value) {
			string[] values = normalize(value);
			return new Vector3(float.Parse(values[0]),
							   float.Parse(values[1]),
							   float.Parse(values[2]));
		}

		public static Vector4 Parse(this Vector4 vector, string value) {
			string[] values = normalize(value);
			return new Vector4(float.Parse(values[0]),
							   float.Parse(values[1]),
							   float.Parse(values[2]),
							   float.Parse(values[3]));
		}

	}

}
