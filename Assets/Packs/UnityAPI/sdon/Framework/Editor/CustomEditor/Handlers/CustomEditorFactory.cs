using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor.Editors;

namespace UnityEditor {

	public class CustomEditorFactory {

		private static List<ICustomFieldEditor> editors;

		static CustomEditorFactory() {

			editors = new List<ICustomFieldEditor>();

			editors.Add(new IntFieldEditor());
			editors.Add(new FloatFieldEditor());
			editors.Add(new BoolFieldEditor());
			editors.Add(new StringFieldEditor());

			editors.Add(new Vector2FieldEditor());
			editors.Add(new Vector3FieldEditor());
			editors.Add(new Vector4FieldEditor());
			editors.Add(new BoundsFieldEditor());
			editors.Add(new ColorFieldEditor());

			editors.Add(new TextureFieldEditor());
			editors.Add(new SpriteFieldEditor());
			editors.Add(new ObjectFieldEditor());

		}

		public static void TryCreateEditor(object target, FieldInfo field, string title, IMonoBehaviourReflection listener) {
			foreach(ICustomFieldEditor fieldEditor in editors) {
				if (fieldEditor.IsEditorType(field.FieldType)) {
					fieldEditor.DoEdit(target, field, title, listener);
					return;
				}
			}
		}

	}

}
