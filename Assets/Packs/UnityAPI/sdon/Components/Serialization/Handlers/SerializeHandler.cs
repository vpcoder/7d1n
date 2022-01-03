using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Serialization {

	public static class SerializeHandler {

		public static string GetSafeData(string data) {

			data = data.Replace("\"", "&quot;");
			data = data.Replace("^", "&cart;");
			data = data.Replace(":", "&ddot;");
			data = data.Replace(";", "&dpnt;");

			data = data.Replace("<", "&lt;");
			data = data.Replace(">", "&gt;");

			return data;
		}

		public static string GetOriginalData(string safeData) {

			safeData = safeData.Replace("&quot;", "\"");
			safeData = safeData.Replace("&cart;", "^");
			safeData = safeData.Replace("&ddot;", ":");
			safeData = safeData.Replace("&dpnt;", ";");

			safeData = safeData.Replace("&lt;", "<");
			safeData = safeData.Replace("&gt;", ">");

			return safeData;
		}

		public static string GetName(Type type) {
			string result = type.Namespace + "." + type.Name;
			
			if (result.EndsWith("`1")) {
				result = result.Substring(0,result.Length-2);
			}

			return GetSafeData(result);
		}

		public static string GetName(string safeName) {
			return GetOriginalData(safeName);
		}

	}

}
