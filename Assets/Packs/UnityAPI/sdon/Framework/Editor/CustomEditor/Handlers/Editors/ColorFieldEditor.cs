using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Color типа
	/// </summary>
	public class ColorFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Color) || type.IsInstanceOfType(typeof(Color));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Color value    = (Color)field.GetValue(target);
			Color newValue = EditorGUILayout.ColorField(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
