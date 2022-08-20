using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей строкового типа (string, String)
	/// </summary>
	public class StringFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(string) || type.IsInstanceOfType(typeof(string));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			string value    = (string)field.GetValue(target);
			string newValue = EditorGUILayout.TextField(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
