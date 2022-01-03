
namespace UnityEngine {

    /// <summary>
    /// Класс-расширение **Matrices** для Matrix объектов Unity 
    /// Авторы: sdon
    /// Дата: 28.03.2017
    /// Версия: 1.0.00
    /// </summary>
    public static class Matrices {

		private static string[] normalize(string value) {
			value = value.Replace("\t", ",");
			value = value.Replace("\n", ",");
			value = value.Replace("\r", "");
			return value.Split(',');
		}

		public static Matrix4x4 Parse(this Matrix4x4 matrix, string value) {
			string[] values = normalize(value);
			Matrix4x4 result = Matrix4x4.identity;
			result.m00 = float.Parse(values[0x0]);
			result.m01 = float.Parse(values[0x1]);
			result.m02 = float.Parse(values[0x2]);
			result.m03 = float.Parse(values[0x3]);
			result.m10 = float.Parse(values[0x4]);
			result.m11 = float.Parse(values[0x5]);
			result.m12 = float.Parse(values[0x6]);
			result.m13 = float.Parse(values[0x7]);
			result.m20 = float.Parse(values[0x8]);
			result.m21 = float.Parse(values[0x9]);
			result.m22 = float.Parse(values[0xa]);
			result.m23 = float.Parse(values[0xb]);
			result.m30 = float.Parse(values[0xc]);
			result.m31 = float.Parse(values[0xd]);
			result.m32 = float.Parse(values[0xe]);
			result.m33 = float.Parse(values[0xf]);
			return result;
		}

	}

}
