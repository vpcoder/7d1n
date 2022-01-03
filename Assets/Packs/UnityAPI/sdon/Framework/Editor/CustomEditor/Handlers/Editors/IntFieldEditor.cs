using System;
using UnityEngine;
using System.Reflection;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей вещественного типа (float, Single)
	/// </summary>
	public class IntFieldEditor: ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(int) || type.IsInstanceOfType(typeof(int));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			int value = (int)field.GetValue(target);

			RangeAttribute range = field.GetAttribute<RangeAttribute>();
			if (range == null) {
				intField(target, field, title, listener, value);
			} else {
				intSlider(target, field, title, listener, value, range);
			}

		}

		private void intField(object target, FieldInfo field, string title, IMonoBehaviourReflection listener, int value) {

			int newValue = EditorGUILayout.IntField(title, value);

			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();

		}

		private void intSlider(object target, FieldInfo field, string title, IMonoBehaviourReflection listener, int value, RangeAttribute range) {

			int newValue = Mathf.RoundToInt(EditorGUILayout.Slider(title, value, range.min, range.max));
			
			if (value == newValue) {
				return;
			}

			field.SetValue(target, newValue);
			listener.OnValidate();
			
		}

	}

}
