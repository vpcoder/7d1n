using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Vector4 типа
	/// </summary>
	public class Vector4FieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Vector4) || type.IsInstanceOfType(typeof(Vector4));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Vector4 value = (Vector4)field.GetValue(target);
			Vector4 newValue = EditorGUILayout.Vector4Field(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
