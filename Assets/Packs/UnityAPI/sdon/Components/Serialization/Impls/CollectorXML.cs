using System;
using System.Collections.Generic;
using Engine.Serialization.IO;
using System.Xml;


namespace Engine.Serialization.Impls {

	/// <summary>
	/// Коллектор-сериализатор на основе XML файлов
	/// </summary>
	/// <typeparam name="K">Тип ключа</typeparam>
	/// <typeparam name="V">Тип значения</typeparam>
	public abstract class CollectorXML<K,V> : SerializeCollector<K,V> 
		where V : class {

		#region Hidden Fields

		/// <summary>
		/// Корневой элемент XML документа
		/// </summary>
		protected const string ROOT_DATA = "root_data";

		/// <summary>
		/// Элемент XML коллекции
		/// </summary>
		protected const string ITEM      = "item";

		#endregion


		#region Abstract Methods

		/// <summary>
		/// Читает элемент из XML документа (Десериализует)
		/// </summary>
		/// <param name="doc">Документ XML, в котором находится элемент</param>
		/// <param name="element">Сериализованный элемент документа XML</param>
		/// <returns>Десериализованный элемент коллекции</returns>
		public abstract V ReadItem(XmlDocument doc, XmlElement element);

		/// <summary>
		/// Создаёт элемент XML по элементу коллекции (Сериализует)
		/// </summary>
		/// <param name="doc">Документ XML, в который будет записан елемент XML</param>
		/// <param name="item">Элемент коллекции</param>
		/// <param name="key">Ключ элемента коллекции</param>
		/// <returns>XML Элемент</returns>
		public abstract XmlElement CreateElement(XmlDocument doc, V item, K key);

		#endregion


		#region Collector Methods

		/// <summary>
		/// Загружает словать dictionary по пути коллекции path
		/// </summary>
		/// <param name="dictionary">Словарь, в который загрузятся данные</param>
		/// <param name="path">Путь, откуда будут браться данные (URL, Путь к файлу, Путь к базе данных и т.д.)</param>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.Xml.XmlException">XmlException</exception>
		/// <exception cref="System.IO.FileNotFoundException">FileNotFoundException</exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">DirectoryNotFoundException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Net.WebException">WebException</exception>
		/// <exception cref="System.UriFormatException">UriFormatException</exception>
		public override void LoadDictionary(ref IDictionary<K, V> dictionary, string path) {

			if (dictionary == null || path == null) {
				throw new ArgumentNullException();
			}

			dictionary.Clear();

			XmlDocument document;
			XmlInputStream input = new XmlInputStream();
			input.open(path);
			document = input.read();

			XmlNodeList elements = document.GetElementsByTagName(ITEM);

			foreach (XmlElement element in elements) {
				V item = ReadItem(document, element);
				K key  = ToKey(item);
				dictionary.Add(key, item);
			}

			input.close();

		}

		/// <summary>
		/// Записывает словать dictionary по пути коллекции path
		/// </summary>
		/// <param name="dictionary">Словарь, из которого запишутся данные</param>
		/// <param name="path">Путь, куда будут писаться данные (URL, Путь к файлу, Путь к базе данных и т.д.)</param>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.Xml.XmlException">XmlException</exception>
		/// <exception cref="System.IO.FileNotFoundException">FileNotFoundException</exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">DirectoryNotFoundException</exception>
		/// <exception cref="System.InvalidOperationException">InvalidOperationException</exception>
		/// <exception cref="System.Net.WebException">WebException</exception>
		/// <exception cref="System.UriFormatException">UriFormatException</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
		/// <exception cref="System.IO.PathTooLongException">PathTooLongException</exception>
		/// <exception cref="System.Security.SecurityException">SecurityException</exception>
		public override void SaveDictionary(IDictionary<K, V> dictionary, string path) {

			if (dictionary == null || path == null) {
				throw new ArgumentNullException();
			}

			XmlDocument document = new XmlDocument();
			XmlElement root = document.CreateElement(ROOT_DATA);
			document.AppendChild(root);

			foreach(K key in collector.Keys){
				root.AppendChild(CreateElement(document, collector[key],key));
			}
			
			XmlOutputStream output = new XmlOutputStream();
			output.open(path);
			output.write(document);
			output.close();

		}

		#endregion

	}

}
