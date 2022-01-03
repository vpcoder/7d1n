using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей логического типа (bool, Boolean)
	/// </summary>
	public class BoolFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(bool) || type.IsInstanceOfType(typeof(bool));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			bool value    = (bool)field.GetValue(target);
			bool newValue = EditorGUILayout.Toggle(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
