using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEditor {

	/// <summary>
	/// Класс-контейнер для MonoBehaviour объекта, позволяющий использовать его основные события (в том числе private и protected)
	/// и поля конкретной реализации (наследника) экземпляра MonoBehaviour класса
	/// </summary>
	public class MonoBehaviourContainer<T> : IMonoBehaviourReflection
		where T : Component
    {

		#region Hidden Fields

		/// <summary>
		/// Хранит целевой объект MonoBehaviour
		/// </summary>
		private T target;
		
		/// <summary>
		/// Хранит таблицу стандартных void методов MonoBehaviour без агрументов (ориентир на версии Unity5)
		/// </summary>
		private Dictionary<string,MethodInfo> methods = new Dictionary<string,MethodInfo>();

		/// <summary>
		/// Хранит список полей целевого объекта
		/// </summary>
		private FieldInfo[] fieldsData;
		private IDictionary<string, FieldInfo> fieldsMap;

		/// <summary>
		/// Список SerializeField полей
		/// </summary>
		private List<SerializedProperty> fieldsSerialized = new List<SerializedProperty>();

		#endregion

		
		#region Methods Constants

		private const string Method_Awake                = "Awake";
		private const string Method_FixedUpdate          = "FixedUpdate";
		private const string Method_LateUpdate           = "LateUpdate";
		private const string Method_OnDestroy            = "OnDestroy";
		private const string Method_OnDisable            = "OnDisable";
		private const string Method_OnDrawGizmos         = "OnDrawGizmos";
		private const string Method_OnDrawGizmosSelected = "OnDrawGizmosSelected";
		private const string Method_OnEnable             = "OnEnable";
		private const string Method_OnGUI                = "OnGUI";
		private const string Method_OnValidate           = "OnValidate";
		private const string Method_Reset                = "Reset";
		private const string Method_Start                = "Start";
		private const string Method_Update               = "Update";

		#endregion

		public Transform Transform
        {
            get
            {
                return target.transform;
            }
        }

		/// <summary>
		/// Возвращает целевой объект
		/// </summary>
		/// <value>Целевой объект</value>
		public T Target {
			get {
				return target;
			}
		}

		/// <summary>
		/// Возвращает список полей целевого объекта
		/// </summary>
		public FieldInfo[] Fields {
			get {
				return fieldsData;
			}
		}

		/// <summary>
		/// Возвращает список сериализованных полей
		/// </summary>
		public List<SerializedProperty> SerializedFields {
			get {
				return fieldsSerialized;
			}
		}

		public MonoBehaviourContainer(T target, SerializedObject serialized) : base() {
		
			this.target = target;
			this.fieldsData = target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			this.fieldsMap = new Dictionary<string, FieldInfo>();
			
			foreach (FieldInfo field in fieldsData) {
				fieldsMap.Add(field.Name, field);
				SerializedProperty property = serialized.FindProperty(field.Name);
				if (property != null) {
					fieldsSerialized.Add(property);
				}
			}

			initBaseMethods();
		}

		public void SetFieldValue<V>(string name, V value)
		{
			var field = GetFieldInfo(name);
			field?.SetValue(Target, value);
		}
		
		public V GetFieldValue<V>(string name)
		{
			var field = GetFieldInfo(name);
			return (V)field?.GetValue(Target);
		}
		
		/// <summary>
		/// Возвращает метаданные поля по имени
		/// </summary>
		/// <param name="name">Имя поля, мета-данные которого необходимо получить</param>
		/// <returns>Возвращает метаданные поля по имени</returns>
		public FieldInfo GetFieldInfo(string name) {
			foreach (FieldInfo field in fieldsData) {
				if (field.Name == name) {
					return field;
				}
			}
			return null;
		}

		/// <summary>
		/// Возвращает ссылку на объект сериализованного поля
		/// </summary>
		/// <param name="name">Имя сериализованного поля в классе MonoBehaviour</param>
		/// <returns>Cсылку на объект сериализованного поля</returns>
		public SerializedProperty GetFieldSerialized(string name) {
			foreach (SerializedProperty field in fieldsSerialized) {
				if (field.name == name) {
					return field;
				}
			}
			return null;
		}

		/// <summary>
		/// Конструирует список методов MonoBehaviour для заданного Target
		/// </summary>
		private void initBaseMethods(){
			
			methods.Add(Method_Awake, GetMethod(Method_Awake));
			methods.Add(Method_FixedUpdate, GetMethod(Method_FixedUpdate));
			methods.Add(Method_LateUpdate, GetMethod(Method_LateUpdate));
			methods.Add(Method_OnDestroy, GetMethod(Method_OnDestroy));
			methods.Add(Method_OnDisable, GetMethod(Method_OnDisable));
			methods.Add(Method_OnDrawGizmos, GetMethod(Method_OnDrawGizmos));
			methods.Add(Method_OnDrawGizmosSelected, GetMethod(Method_OnDrawGizmosSelected));
			methods.Add(Method_OnEnable, GetMethod(Method_OnEnable));
			methods.Add(Method_OnGUI, GetMethod(Method_OnGUI));
			methods.Add(Method_OnValidate, GetMethod(Method_OnValidate));
			methods.Add(Method_Reset, GetMethod(Method_Reset));
			methods.Add(Method_Start, GetMethod(Method_Start));
			methods.Add(Method_Update, GetMethod(Method_Update));
			
		}

		/// <summary>
		/// Возвращает метаданные метода name из класса T
		/// </summary>
		/// <param name="name">Имя метода</param>
		/// <returns>Метаданные метода name из класса T</returns>
		private MethodInfo GetMethod(string name){
			return typeof(T).GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		}

		/// <summary>
		/// Возвращает метаданные поля name из класса T
		/// </summary>
		/// <param name="name">Имя поля</param>
		/// <returns>Метаданные поля name из класса T</returns>
		private FieldInfo GetField(string name)
		{
			return fieldsMap[name];
		}

		/// <summary>
		/// Пытается вызвать указанный метод
		/// </summary>
		/// <param name="name">Имя метода, который надо вызвать</param>
		private void invoke(string name){
			
			if(!methods.ContainsKey(name)){
				return;
			}
			
			MethodInfo method = methods[name];
			
			if(method == null){
				return;
			}
			
			method.Invoke(target, new object[]{ });
			
		}
		

		#region MonoBehaviour Methods
		
		public void Awake() {
			invoke(Method_Awake);
		}

		public void FixedUpdate() {
			invoke(Method_FixedUpdate);
		}

		public void LateUpdate() {
			invoke(Method_LateUpdate);
		}

		public void OnDestroy() {
			invoke(Method_OnDestroy);
		}

		public void OnDisable() {
			invoke(Method_OnDisable);
		}

		public void OnDrawGizmos() {
			invoke(Method_OnDrawGizmos);
		}

		public void OnDrawGizmosSelected() {
			invoke(Method_OnDrawGizmosSelected);
		}

		public void OnEnable() {
			invoke(Method_OnEnable);
		}

		public void OnGUI() {
			invoke(Method_OnGUI);
		}

		public void OnValidate() {
			invoke(Method_OnValidate);
		}

		public void Reset() {
			invoke(Method_Reset);
		}

		public void Start() {
			invoke(Method_Start);
		}

		public void Update() {
			invoke(Method_Update);
		}

		#endregion

	}

}
