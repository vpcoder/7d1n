using System;
using UnityEngine;

namespace Engine {

	/// <summary>
	/// Атрибут помечающий поле как "подлежит сериализации". Помеченное таким образом поле будет обрабатываться сериализатором (все остальные поля будут игнорироваться)
	/// <example>
	/// Пример использования:
	/// <code>
	/// ...
	/// [SerializeData] private int speed;
	/// ...
	/// </code>
	/// </example>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
	public class SerializeData : PropertyAttribute {

		public bool isSimpleType = true;

		/// <summary>
		/// Конструктор атрибута
		/// </summary>
		public SerializeData() : base() { }

		public SerializeData(bool isSimpleType) : base() {
			this.isSimpleType = isSimpleType;
		}

	}

}
