using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Texture типа
	/// </summary>
	public class TextureFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Texture) || type == typeof(Texture2D)
				|| type.IsInstanceOfType(typeof(Texture)) || type.IsInstanceOfType(typeof(Texture2D));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Texture texture = (Texture)field.GetValue(target);
			Rect fieldBounds;

			if (texture != null && texture.width >= 96) {
				fieldBounds = GUILayoutUtility.GetRect(96, 96);
			} else {
				fieldBounds = GUILayoutUtility.GetRect(32, 32);
			}
			/*
			Texture textureNew = (Texture)EditorGUI.ObjectField(fieldBounds, title, (Texture)field.GetValue(target), typeof(Texture));
			
			if (textureNew == null && texture != null) {
				field.SetValue(target, textureNew);
				listener.OnValidate();
				return;
			}

			if (textureNew==texture || textureNew.Equals(texture)) {
				return;
			}

			field.SetValue(target, textureNew);
			listener.OnValidate();
			*/
		}

	}

}
