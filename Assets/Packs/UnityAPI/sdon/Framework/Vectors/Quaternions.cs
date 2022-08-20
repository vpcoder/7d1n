using System;
using System.Collections.Generic;

namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **Quaternions** для Quaternion объектов Unity 
    /// Авторы: sdon
    /// Дата: 23.03.2017
    /// Версия: 1.0.00
    /// </summary>
    public static class Quaternions {

		private static string[] normalize(string value) {

			if (value.StartsWith("(")) {
				value = value.Substring(1, value.Length - 1);
			}
			if (value.EndsWith(")")) {
				value = value.Substring(0, value.Length - 1);
			}

			return value.Split(',');

		}

		public static Quaternion Parse(this Quaternion rotate, string value) {
			string[] values = normalize(value);
			return new Quaternion(float.Parse(values[0]),
								  float.Parse(values[1]),
								  float.Parse(values[2]),
								  float.Parse(values[3]));
		}

	}

}
