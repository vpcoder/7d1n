using System;
using UnityEngine;

namespace UnityEditor.Sdon {

	public static partial class Tables {

		public static bool BoolEditField(bool value, int widthLabel = 20, int widthArea = 20) {
			EditorGUILayout.BeginHorizontal();

			value = EditorGUILayout.Toggle(value, GUILayout.Height(20), GUILayout.Width(widthArea));

			Texture2D icon = IconsFactory.Instance.GetIcon(value ? Icons.Edit : Icons.EditOff);
			GUILayout.Label(icon, GUILayout.Width(widthLabel), GUILayout.Height(20));

			EditorGUILayout.EndHorizontal();
			return value;
		}

		public static T GameObjectField<T>(T gameObject, bool findInThisScene = false, string caption = "", int captionWidth = 40) {
			EditorGUILayout.BeginHorizontal();
			if(caption!="")
				GUILayout.Label(caption, GUILayout.Width(captionWidth), GUILayout.Height(20));
			gameObject = (T)Convert.ChangeType(EditorGUILayout.ObjectField(gameObject as UnityEngine.Object, typeof(T), findInThisScene),typeof(T));
            EditorGUILayout.EndHorizontal();
			return gameObject;
        }

		public static Vector3 Vector3Field(Vector3 vector, string caption = "", int captionWidth = 20) {
			EditorGUILayout.BeginHorizontal();
			if (caption != "")
				GUILayout.Label(caption, GUILayout.Width(captionWidth), GUILayout.Height(20));
			GUILayout.Label("X:", GUILayout.Width(20), GUILayout.Height(20));
			vector.x = EditorGUILayout.FloatField(vector.x, GUILayout.MaxWidth(32));
			GUILayout.Label("Y:", GUILayout.Width(20), GUILayout.Height(20));
			vector.y = EditorGUILayout.FloatField(vector.y, GUILayout.MaxWidth(32));
			GUILayout.Label("Z:", GUILayout.Width(20), GUILayout.Height(20));
			vector.z = EditorGUILayout.FloatField(vector.z, GUILayout.MaxWidth(32));
			EditorGUILayout.EndHorizontal();
			return vector;
		}

		public static Vector4 Vector4Field(Vector4 vector, string caption = "", int captionWidth = 20) {
			EditorGUILayout.BeginHorizontal();
			if (caption != "")
				GUILayout.Label(caption, GUILayout.Width(captionWidth), GUILayout.Height(20));
			GUILayout.Label("X:", GUILayout.Width(20), GUILayout.Height(20));
			vector.x = EditorGUILayout.FloatField(vector.x, GUILayout.MaxWidth(32));
			GUILayout.Label("Y:", GUILayout.Width(20), GUILayout.Height(20));
			vector.y = EditorGUILayout.FloatField(vector.y, GUILayout.MaxWidth(32));
			GUILayout.Label("Z:", GUILayout.Width(20), GUILayout.Height(20));
			vector.z = EditorGUILayout.FloatField(vector.z, GUILayout.MaxWidth(32));
			GUILayout.Label("W:", GUILayout.Width(20), GUILayout.Height(20));
			vector.w = EditorGUILayout.FloatField(vector.w, GUILayout.MaxWidth(32));
			EditorGUILayout.EndHorizontal();
			return vector;
		}

		public static Vector2 Vector2Field(Vector2 vector, string caption = "", int captionWidth=20) {
			EditorGUILayout.BeginHorizontal();
			if(caption!="")
				GUILayout.Label(caption, GUILayout.Width(captionWidth), GUILayout.Height(20));
			GUILayout.Label("X:", GUILayout.Width(20), GUILayout.Height(20));
			vector.x = EditorGUILayout.FloatField(vector.x, GUILayout.MaxWidth(32));
			GUILayout.Label("Y:", GUILayout.Width(20), GUILayout.Height(20));
			vector.y = EditorGUILayout.FloatField(vector.y, GUILayout.MaxWidth(32));
			EditorGUILayout.EndHorizontal();
			return vector;
		}

		public static int IntField(string caption, int value, int widthLabel = 32, int widthArea = 20) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(caption, GUILayout.Width(widthLabel), GUILayout.Height(20));
			value = EditorGUILayout.IntField(value, GUILayout.Width(widthArea), GUILayout.Height(20));
			EditorGUILayout.EndHorizontal();
			return value;
		}

		public static float FloatField(string caption, float value, int widthLabel = 32, int widthArea = 20) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(caption, GUILayout.Width(widthLabel), GUILayout.Height(20));
			value = EditorGUILayout.FloatField(value, GUILayout.Width(widthArea), GUILayout.Height(20));
			EditorGUILayout.EndHorizontal();
			return value;
		}

		public static string TextField(string caption, string value, int widthLabel = 32, int widthArea = 20) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(caption, GUILayout.Width(widthLabel), GUILayout.Height(20));
			value = EditorGUILayout.TextField(value, GUILayout.Width(widthArea), GUILayout.Height(20));
			EditorGUILayout.EndHorizontal();
			return value;
		}

		public static void Splitter(Color rgb, float thickness = 1) {

			if (splitter == null) {
				splitter = new GUIStyle();
				splitter.normal.background = EditorGUIUtility.whiteTexture;
				splitter.stretchWidth = true;
				splitter.margin = new RectOffset(0, 0, 1, 1);
			}

			Rect position = GUILayoutUtility.GetRect(GUIContent.none, splitter, GUILayout.Height(thickness));

			if (Event.current.type == EventType.Repaint) {
				Color restoreColor = GUI.color;
				GUI.color = rgb;
				splitter.Draw(position, false, false, false, false);
				GUI.color = restoreColor;
			}
		}

	}

}
