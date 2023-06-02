using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace UnityEditor {

	/// <summary>
	/// Абстрактный класс пользовательского редактора
	/// </summary>
	public abstract class CustomEditorT<T> : Editor, ICustomEditor
		where T : Component
    {

		#region Hidden Fields

		/// <summary>
		/// Хранит контейнер для целевого класса
		/// </summary>
		protected new MonoBehaviourContainer<T> target;

		/// <summary>
		/// Список фейдеров для каждого из документированного поля
		/// </summary>
		private Dictionary<FieldInfo, bool> commentsVisible = new Dictionary<FieldInfo, bool>();

		/// <summary>
		/// Иконка компонента, если требуется
		/// </summary>
		private Texture icon;

		/// <summary>
		/// Показывает, надо ли отображать редактор-по умолчанию (первоначальный редактор, построенный на SerializedField атрибутах)
		/// </summary>
		private bool showEditor = true;

		/// <summary>
		/// Показывает, надо ли отображать документацию
		/// </summary>
		private bool showDocs = false;

		#endregion

		/// <summary>
		/// Изменяет состояние видимости вкладки с редактором по умолчанию
		/// </summary>
		public bool ShowEditor {
			get {
				return showEditor;
			}
			set {
				this.showEditor = value;
			}
		}

		/// <summary>
		/// Иконка компонента, может быть null
		/// </summary>
		public Texture Icon {
			get {
				return icon;
			}
			set {
				this.icon = value;
			}
		}


		#region Editor Sections

		/// <summary>
		/// Выполняет построение секции описания редактора
		/// </summary>
		private void DoDescriptionSection() {

			string description = GetDescription();

			if (Icon != null) {
				Rect rect = EditorGUILayout.GetControlRect(description!=null, Icon.height); // если есть иконка, рисуем её
				GUI.DrawTexture(new Rect(rect.x, rect.y, Icon.width, icon.height), Icon, ScaleMode.ScaleToFit);
			}
			
			if (description != null) {
				APILayout.LabelBox(description); // если есть описание редактора, выводим его
			}

		}

		/// <summary>
		/// Выполняет построения секции дополнительной информации (произвольно текста) для редактора
		/// </summary>
		private void DoTextInfoSection() {

			string textInfo = GetTextInfo();

			if (textInfo != null) {
				APILayout.InfoBox(textInfo);
			}

		}

		/// <summary>
		/// Выполняет построение секции редактирования
		/// </summary>
		private void DoEditorSection() {

			EditorVisibleType visibleType = GetEditorVisibleType();

			switch (visibleType)
			{
				case EditorVisibleType.HideInFader:
					showEditor = EditorGUILayout.Foldout(showEditor, "Editor | Редактор");
					if (showEditor)
						OnDefaultEditor();
					break;
				case EditorVisibleType.Show:
					OnDefaultEditor();
					break;
			}

			showDocs = EditorGUILayout.Foldout(showDocs, "Documentation | Документация");
			if (showDocs) {
				OnDocsEditor();
			}

			OnAdditionEditor();
		}

		#endregion


		#region Unity Events

		private void OnEnable() {
			
			if(base.target == null)
				return;
			
			this.target = new MonoBehaviourContainer<T>(base.target as T, serializedObject);

			foreach (FieldInfo field in target.Fields) {
				commentsVisible[field] = false;
			}
			
			OnStart();

		}

		public override void OnInspectorGUI() {
			DoDescriptionSection();
			DoTextInfoSection();
			DoEditorSection();
		}

		#endregion


		#region Functions

		/// <summary>
		/// Должен вернуть тип видимости редактора по умолчанию (редактор для класса, который формирует Unity по SerializeField/public полям). По умолчанию скрыт во вкладке "Редактор"
		/// </summary>
		/// <returns></returns>
		public virtual EditorVisibleType GetEditorVisibleType() {
			return EditorVisibleType.HideInFader;
		}

		/// <summary>
		/// Должен вернуть информацию по классу
		/// </summary>
		public virtual string GetTextInfo() {
			return null;
		}

		/// <summary>
		/// Должен вернуть описание класса-редактора. По умолчанию возвращает null.
		/// </summary>
		public virtual string GetDescription() {
			return null;
		}

		public virtual void OnDocsEditor() {

			foreach (FieldInfo field in this.target.Fields) {

				if (!field.IsUnityEditorField() || field.GetAttribute<IgnoreDocAttribute>()!=null) {
					continue;
				}

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(field.Name, GetEditorLabel(field));
				CheckEditorComment(field);

			}

		}

		/// <summary>
		/// Проверяет, и строит метку для поля в редакторе
		/// </summary>
		/// <param name="field">Поле, для которого строится метка</param>
		protected string GetEditorLabel(FieldInfo field) {

			CaptionAttribute attribute = field.GetAttribute<CaptionAttribute>();
			if (attribute != null) {
				return attribute.caption;
			}

			return field.Name;

		}

		/// <summary>
		/// Проверяет и, при необходимости, строит блок комментариев для указанного поля
		/// </summary>
		/// <param name="field">Поле, для которого строится блок комментариев</param>
		protected void CheckEditorComment(FieldInfo field) {

			CommentsAttribute comments = field.GetAttribute<CommentsAttribute>();
			if (comments == null) {
				EditorGUILayout.EndHorizontal();
				return;
			}
			
			Rect last = GUILayoutUtility.GetLastRect();
			Rect buttonRect = new Rect(Screen.width-43,last.y,21,18);

			if (GUI.Button(buttonRect, IconsFactory.Instance.GetIcon(commentsVisible[field] ? Icons.InfoOff : Icons.Info))) {
				commentsVisible[field] = !commentsVisible[field];
			}

			EditorGUILayout.EndHorizontal();

			if (commentsVisible[field]) {
				APILayout.CommentBox(comments.comment);
			}

		}

		/// <summary>
		/// Изменяет редактор по умолчанию
		/// </summary>
		public virtual void OnDefaultEditor() {
			base.OnInspectorGUI();
		}

		/// <summary>
		/// Метод дополнительного редактора. Внутри метода можно добавлять элементы редактирования CustomEditor для целевого класса.
		/// </summary>
		public virtual void OnAdditionEditor() { }

		/// <summary>
		/// Метод вызывается в момент инициализации CustomEditor-а.
		/// </summary>
		public virtual void OnStart() { }

		/// <summary>
		/// Продолжает метод Update у MonoBehaviour
		/// </summary>
		public virtual void OnUpdate() { }

		#endregion

	}

}
