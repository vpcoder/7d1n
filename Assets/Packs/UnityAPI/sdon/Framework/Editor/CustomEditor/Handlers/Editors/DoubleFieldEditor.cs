using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей вещественного типа (float, Single)
	/// </summary>
	public class DoubleFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(double) || type.IsInstanceOfType(typeof(double));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			double value    = (double)field.GetValue(target);
			double newValue = EditorGUILayout.FloatField(title, (float)value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
