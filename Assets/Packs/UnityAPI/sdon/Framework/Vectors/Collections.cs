using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine {

	public static class VectorCollectionsAdditions {

		#region Vector2 -> Vector3

		public static IEnumerable<Vector3> ToVector3(this IEnumerable<Vector2> collection) {
			foreach (var item in collection)
				yield return new Vector3(item.x, item.y);
		}

		public static IList<Vector3> ToVector3(this IList<Vector2> collection) {
			return new List<Vector3>(ToVector3((IEnumerable<Vector2>)collection));
		}

		#endregion

		#region Vector2 -> Vector4

		public static IEnumerable<Vector4> ToVector4(this IEnumerable<Vector2> collection) {
			foreach (var item in collection)
				yield return new Vector4(item.x, item.y);
		}

		public static IList<Vector4> ToVector4(this IList<Vector2> collection) {
			return new List<Vector4>(ToVector4((IEnumerable<Vector2>)collection));
		}

		#endregion

		#region Vector3 -> Vector4

		public static IEnumerable<Vector4> ToVector4(this IEnumerable<Vector3> collection) {
			foreach (var item in collection)
				yield return new Vector4(item.x, item.y);
		}

		public static IList<Vector4> ToVector4(this IList<Vector3> collection) {
			return new List<Vector4>(ToVector4((IEnumerable<Vector3>)collection));
		}

		#endregion

	}

}
