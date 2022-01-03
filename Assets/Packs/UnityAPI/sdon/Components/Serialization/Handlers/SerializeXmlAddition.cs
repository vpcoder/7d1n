using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;

namespace Engine.Serialization {

    /// <summary>
    /// Класс-расширение **SerializeHandlerAddition** для XmlElement объекта System.Xml 
    /// Авторы: sdon
    /// Дата: 07.03.2017
    /// Версия: 1.0.0
    /// </summary>
    public static class SerializeXmlAddition {

		/// <summary>
		/// Записывает все, помеченные аттрибутом SerializeData, поля экземпляра класса data в XML элемент element
		/// </summary>
		/// <param name="element">Элемент XML в который запишутся сериализованные данные</param>
		/// <param name="doc">Ссылка на XML документ, в котором находится element</param>
		/// <param name="data">Объект, содержимое которого необходимо записать</param>
		/// <param name="tagName">Устанавливает значение тега "name", за которым будет закреплена вся сериализованная информация внутри xml элемента</param>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <exception cref="System.Xml.XmlException">XmlException</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// MyDataClass data   = MyDataClassFactory.GetData(); // получаем какие то данные, некоего типа
		/// XmlElement element = xmlDocument.CreateElement("MyData"); // создаём какой нибудь элемент для этих данных
		/// element.PutSerializeData(xmlDocument, data); // записывает содержимое data класса MyDataClass в элемент с меткой "MyData"
		/// </code>
		/// </example>
		public static void PutSerializeData(this XmlElement element, XmlDocument doc, object data, string tagName) {

			XmlElement fieldItem;
			XmlElement classItem = doc.CreateElement("field");
			classItem.SetAttribute("type", SerializeHandler.GetName(data.GetType()));
			classItem.SetAttribute("name", tagName);
			element.AppendChild(classItem);

			foreach (FieldInfo field in data.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				
				SerializeData serializeData = field.GetAttribute<SerializeData>();
				if (serializeData==null) {
					continue;
				}

				if (!serializeData.isSimpleType) {
					classItem.PutSerializeData(doc, field.GetValue(data), field.Name);
					continue;
				}

				string value = SerializeSerializer.Serialize(field.GetValue(data), field.FieldType);

				fieldItem = doc.CreateElement("field");
				fieldItem.SetAttribute("type", SerializeHandler.GetName(field.FieldType));
				fieldItem.SetAttribute("name", field.Name);
				classItem.AppendChild(fieldItem);

				if (value != null) {
					fieldItem.InnerXml = value;
				} else {
					fieldItem.InnerXml = "NULL";
				}

			}

		}

		/// <summary>
		/// Создаёт экземпляр класса T, читает element и заполняет помеченные аттрибутом SerializeData поля экземпляра класса, после чего выдаёт ссылку на него
		/// </summary>
		/// <typeparam name="T">Тип класса, который необходимо получить при десериализации класса из элемента element</typeparam>
		/// <param name="element">"XML элемент с данными класса</param>
		/// <param name="doc">XML документ в котором находится elemen</param>
		/// <returns>Возвращает ссылку на экземпляр класса T с заполненными данными из XML элемента element</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Reflection.TargetInvocationException">TargetInvocationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.IO.FileNotFoundException">FileNotFoundException</exception>
		/// <exception cref="System.IO.FileLoadException">FileLoadException</exception>
		/// <exception cref="System.BadImageFormatException">BadImageFormatException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// XmlElement  element = ...; // получаем XML элемент с данными сериализации
		/// MyDataClass data = element.GetSerializeData<MyDataClass>(xmlDocument); // Создаём инстанс объекта с заполненными полями
		/// </code>
		/// </example>
		public static T GetSerializeData<T>(this XmlElement element, XmlDocument doc) {
			Type   typeName  = Type.GetType(SerializeHandler.GetName(element.GetAttribute("type")), true);
			object classItem = Activator.CreateInstance(typeName);
			return GetSerializeData<T>(element, doc, classItem);
		}

		/// <summary>
		/// Читает element и заполняет помеченные аттрибутом SerializeData поля экземпляра класса , после чего выдаёт ссылку на него
		/// </summary>
		/// <typeparam name="T">Тип класса, который необходимо получить при десериализации класса из элемента element</typeparam>
		/// <param name="element">"XML элемент с данными класса</param>
		/// <param name="doc">XML документ в котором находится elemen</param>
		/// <returns>Возвращает ссылку на экземпляр класса T с заполненными данными из XML элемента element</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Reflection.TargetInvocationException">TargetInvocationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.IO.FileNotFoundException">FileNotFoundException</exception>
		/// <exception cref="System.IO.FileLoadException">FileLoadException</exception>
		/// <exception cref="System.BadImageFormatException">BadImageFormatException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// XmlElement  element = ...; // получаем XML элемент с данными сериализации
		/// MyDataClass data = new MyDataClass(...); // создаём, или получаем некий экземпляр объекта
		/// element.GetSerializeData<MyDataClass>(xmlDocument,data); // Заполняем поля объекта из данных сериализации
		/// </code>
		/// </example>
		public static T GetSerializeData<T>(this XmlElement element, XmlDocument doc, object classItem) {
			Type typeName = Type.GetType(SerializeHandler.GetName(element.GetAttribute("type")), true);

			try {

				foreach (FieldInfo field in typeName.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {

					SerializeData serializeData = field.GetAttribute<SerializeData>();

					if (serializeData == null) {
						continue;
					}

					if (!serializeData.isSimpleType) {
						object subItem = GetSerializeData<T>(element.findFirstElementByAttribute("name", field.Name), doc);
						ReflectorHandler.SetFieldValue(classItem, field.Name, subItem);
						continue;
					}

					try {
						string value = element.findFirstElementByAttribute("name", field.Name).InnerXml;
						if (value == null || value == "NULL") {
							ReflectorHandler.SetFieldValue(classItem, field.Name, null);
						} else {
							ReflectorHandler.SetFieldValue(classItem, field.Name, SerializeParser.Parse(value, field.FieldType));
						}
					} catch (Exception) { }

				}

			} catch (Exception) { }

			return (T)classItem;
		}

		/// <summary>
		/// Читает element и заполняет помеченные аттрибутом SerializeData поля экземпляра класса , после чего выдаёт ссылку на него
		/// </summary>
		/// <param name="element">"XML элемент с данными класса</param>
		/// <param name="doc">XML документ в котором находится elemen</param>
		/// <returns>Возвращает ссылку на новый экземпляр класса classItem с заполненными данными из XML элемента element</returns>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Reflection.TargetInvocationException">TargetInvocationException</exception>
		/// <exception cref="System.TypeLoadException">TypeLoadException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.IO.FileNotFoundException">FileNotFoundException</exception>
		/// <exception cref="System.IO.FileLoadException">FileLoadException</exception>
		/// <exception cref="System.BadImageFormatException">BadImageFormatException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		/// XmlElement  element = ...; // получаем XML элемент с данными сериализации
		/// MyDataClass data = new MyDataClass(...); // создаём, или получаем некий экземпляр объекта
		/// element.GetSerializeData(xmlDocument,data); // Заполняем поля объекта из данных сериализации
		/// </code>
		/// </example>
		public static object GetSerializeData(this XmlElement element, XmlDocument doc, object classItem) {
			return GetSerializeData<object>(element,doc,classItem);
		}

	}

}
