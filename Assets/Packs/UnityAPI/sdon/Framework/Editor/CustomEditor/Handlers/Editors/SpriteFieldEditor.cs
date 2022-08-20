using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Sprite типа
	/// </summary>
	public class SpriteFieldEditor : ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Sprite) || type.IsInstanceOfType(typeof(Sprite));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Sprite sprite = (Sprite)field.GetValue(target);
			Rect fieldBounds;

			if (sprite != null && sprite.texture != null && sprite.texture.width >= 96) {
				fieldBounds = GUILayoutUtility.GetRect(96, 96);
			} else {
				fieldBounds = GUILayoutUtility.GetRect(32, 32);
			}
			/*
			Sprite spriteNew = (Sprite)EditorGUI.ObjectField(fieldBounds, title, (Sprite)field.GetValue(target), typeof(Sprite));

			if (spriteNew == null && sprite != null) {
				field.SetValue(target, spriteNew);
				listener.OnValidate();
				return;
			}

			if (spriteNew == sprite || spriteNew.Equals(sprite)) {
				return;
			}

			field.SetValue(target, spriteNew);
			listener.OnValidate();
			*/

		}

	}

}
