using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Rect типа
	/// </summary>
	public class RectFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Rect) || type.IsInstanceOfType(typeof(Rect));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			Rect value    = (Rect)field.GetValue(target);
			Rect newValue = EditorGUILayout.RectField(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

	}

}
