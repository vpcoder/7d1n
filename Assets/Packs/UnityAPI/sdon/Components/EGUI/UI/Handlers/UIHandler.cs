using System;
using UnityEngine;

namespace UnityEngine {

	public class UIHandler {

		private static Canvas mainLink = null;

		public static Canvas Canvas {

			get {

				if (mainLink == null) {

					mainLink = ObjectFinder.Get<Canvas>("Canvas");

#if UNITY_EDITOR
					if (mainLink == null) {
						Debug.LogError("Не удалось найти Canvas объект в сцене!");
					}
#endif

				}

				return mainLink;

			}

		}

	}

}
