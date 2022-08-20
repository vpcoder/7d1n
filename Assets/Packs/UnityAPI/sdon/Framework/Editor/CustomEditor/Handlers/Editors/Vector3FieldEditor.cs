using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Vector3 типа
	/// </summary>
	public class Vector3FieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Vector3) || type.IsInstanceOfType(typeof(Vector3));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Vector3 value = (Vector3)field.GetValue(target);
			Vector3 newValue = EditorGUILayout.Vector3Field(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
