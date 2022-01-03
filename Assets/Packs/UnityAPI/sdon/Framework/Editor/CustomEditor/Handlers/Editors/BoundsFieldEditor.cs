using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Bounds типа
	/// </summary>
	public class BoundsFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Bounds) || type.IsInstanceOfType(typeof(Bounds));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Bounds value    = (Bounds)field.GetValue(target);
			Bounds newValue = EditorGUILayout.BoundsField(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
