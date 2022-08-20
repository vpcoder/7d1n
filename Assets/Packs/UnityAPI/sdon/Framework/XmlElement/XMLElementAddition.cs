using UnityEngine;

namespace System.Xml {

    /// <summary>
    /// Класс-расширение **XMLElementAddition** для XmlElement объекта System.Xml 
    /// Авторы: sdon
    /// Дата: 16.10.2016
    /// Обновление: 14.02.2017
    /// Обновление: 29.03.2017
    /// Версия: 1.00.003
    /// </summary>
    public static class XMLElementAddition {

		/// <summary>
		/// Ищет и возвращает первый Xml элемент по тегу findElementTagName
		/// </summary>
		/// <param name="findElementTagName">Тег искомого элемента</param>
		/// <returns>Возвращает первый XmlElement объект, найденный по тегу findElementTagName</returns>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		<test>
		///			<data id="1"></data>
		///			<data id="2"></data>
		///		</test>
		/// ...
		/// XmlElement data = test.findFirstElement("data"); // id=1
		/// </code>
		/// </example>
		public static XmlElement findFirstElement(this XmlElement element, string findElementTagName) {

			XmlNodeList list = element.GetElementsByTagName(findElementTagName);

			if (list.Count <= 0) {

#if UNITY_EDITOR
				Debug.LogWarning(element.InnerXml);
				Debug.LogError("Отсутствует блок '" + findElementTagName + "'!");
#endif

				throw new Exception("Отсутствует блок '" + findElementTagName + "'");
			}

			return (XmlElement)list.Item(0);

		}

		/// <summary>
		/// Ищет элемент по значению атрибута и возвращает первый найденный.
		/// </summary>
		/// <param name="element">Элемент внутри которого проводится поиск</param>
		/// <param name="attributeName">Имя искомого атрибута</param>
		/// <param name="attributeValue">Значение искомого атрибута</param>
		/// <returns>Возвращает найденный атрибут. Если атрибут не был найден, вернёт nullpointer/returns>
		/// <example>
		/// Пример использования:
		/// <code>
		/// XmlElement test = ... // <test><item value="1"/><item value="2"/></test>
		/// XmlElement item1 = test.findFirstElementByAttribute("value","1");
		/// XmlElement item2 = test.findFirstElementByAttribute("value","2");
		/// </code>
		/// </example>
		public static XmlElement findFirstElementByAttribute(this XmlElement element, string attributeName, string attributeValue) {

			foreach (XmlElement item in element.ChildNodes) {

				try {

					if (item.GetAttribute(attributeName) == attributeValue) {
						return item;
					}

				} catch (Exception e) {

#if UNITY_EDITOR
					Debug.LogWarning(element.InnerXml);
					Debug.LogError("Отсутствует искомый атрибут '"+attributeName+"'!");
					
#endif

					throw e;

				}

			}

			return null;

		}

		/// <summary>
		/// Ищет и возвращает первый Xml элемент по тегу findElementTagName
		/// </summary>
		/// <param name="findElementTagName">Тег искомого элемента</param>
		/// <returns>Возвращает первый XmlElement объект, найденный по тегу findElementTagName</returns>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		<test>
		///			<data id="1"></data>
		///			<data id="2"></data>
		///		</test>
		/// ...
		/// XmlElement data = test.findFirstElement("data"); // id=1
		/// </code>
		/// </example>
		public static XmlElement findFirstElement(this XmlDocument document, string findElementTagName) {

			XmlNodeList list = document.GetElementsByTagName(findElementTagName);

			if (list.Count <= 0) {

#if UNITY_EDITOR
				Debug.LogError("Отсутствует блок '" + findElementTagName + "'!");
#endif

				throw new Exception("Отсутствует блок '" + findElementTagName + "'");
			}

			return (XmlElement)list.Item(0);

		}

		/// <summary>
		/// Ищет и возвращает первый Xml элемент по тегу findElementTagName
		/// </summary>
		/// <param name="findElementTagName">Тег искомого элемента</param>
		/// <returns>Возвращает первый XmlElement объект, найденный по тегу findElementTagName</returns>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		<test>
		///			<data id="1"></data>
		///			<data id="2"></data>
		///		</test>
		/// ...
		/// XmlElement data = test.findLastElement("data"); // id=2
		/// </code>
		/// </example>
		public static XmlElement findLastElement(this XmlElement element, string findElementTagName) {

			XmlNodeList list = element.GetElementsByTagName(findElementTagName);

			if (list.Count <= 0) {

#if UNITY_EDITOR
				Debug.LogError("Отсутствует блок '" + findElementTagName + "'!");
#endif

				throw new Exception("Отсутствует блок '" + findElementTagName + "'");
			}

			return (XmlElement)list.Item(list.Count-1);

		}

		/// <summary>
		/// Ищет и возвращает первый Xml элемент по тегу findElementTagName
		/// </summary>
		/// <param name="findElementTagName">Тег искомого элемента</param>
		/// <returns>Возвращает первый XmlElement объект, найденный по тегу findElementTagName</returns>
		/// <exception cref="System.Exception">Exception</exception>
		/// <example>
		/// Пример использования:
		/// <code>
		///		<test>
		///			<data id="1"></data>
		///			<data id="2"></data>
		///		</test>
		/// ...
		/// XmlElement data = test.findLastElement("data"); // id=2
		/// </code>
		/// </example>
		public static XmlElement findLastElement(this XmlDocument document, string findElementTagName) {

			XmlNodeList list = document.GetElementsByTagName(findElementTagName);

			if (list.Count <= 0) {

#if UNITY_EDITOR
				Debug.LogError("Отсутствует блок '" + findElementTagName + "'!");
#endif

				throw new Exception("Отсутствует блок '" + findElementTagName + "'");
			}

			return (XmlElement)list.Item(list.Count - 1);

		}

	}

}
