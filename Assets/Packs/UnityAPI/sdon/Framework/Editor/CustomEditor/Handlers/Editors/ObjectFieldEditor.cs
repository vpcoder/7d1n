using System;
using UnityEngine;
using System.Reflection;
using Object = UnityEngine.Object;

namespace UnityEditor.Editors {

	/// <summary>
	/// Редактор полей Object типа
	/// </summary>
	public class ObjectFieldEditor : ICustomFieldEditor {

		public bool IsEditorType(Type type) {
			return type == typeof(Object) || type.IsInstanceOfType(typeof(Object));
		}

		public void DoEdit(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {

			//Object obj;

			//try {
			//	obj = (Object)field.GetValue(target);
			//} catch (Exception) { return; }

			//Rect fieldBounds;

			//fieldBounds = GUILayoutUtility.GetRect(64, 64);
			
			//obj = (Object)EditorGUI.ObjectField(fieldBounds, title, (Object)field.GetValue(target), typeof(Object));
			//field.SetValue(target, obj);

		}

	}

}
