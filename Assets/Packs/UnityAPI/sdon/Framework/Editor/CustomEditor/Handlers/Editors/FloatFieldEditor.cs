using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей вещественного типа (float, Single)
	/// </summary>
	public class FloatFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(float) || type.IsInstanceOfType(typeof(float));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			float value = (float)field.GetValue(target);

			RangeAttribute range = field.GetAttribute<RangeAttribute>();
			if (range == null) {
				floatField(target, field, title, listener, value);
			} else {
				floatSlider(target, field, title, listener, value, range);
			}

		}

		private void floatField(object target, FieldInfo field, string title, IMonoBehaviourReflection listener, float value) {
			
			float newValue = EditorGUILayout.FloatField(title, value);
			
			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

		private void floatSlider(object target, FieldInfo field, string title, IMonoBehaviourReflection listener, float value, RangeAttribute range) {

			float newValue = EditorGUILayout.Slider(title, value, range.min, range.max);

			if (value == newValue) {
				return;
			}

			GUI.changed = true;

			field.SetValue(target, newValue);
			listener.OnValidate();
			
		}

	}

}
