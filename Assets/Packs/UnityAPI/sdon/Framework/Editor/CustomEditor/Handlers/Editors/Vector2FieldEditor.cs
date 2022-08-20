using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Vector2 типа
	/// </summary>
	public class Vector2FieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Vector2) || type.IsInstanceOfType(typeof(int));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Vector2 value    = (Vector2)field.GetValue(target);
			Vector2 newValue = EditorGUILayout.Vector2Field(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
