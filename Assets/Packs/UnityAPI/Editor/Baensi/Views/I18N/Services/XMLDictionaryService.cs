using System.Xml;
using System.IO;
using UnityEditor.Sdon.I18N.Model;
using Engine.I18N;
using Dictionary = UnityEditor.Sdon.I18N.Model.Dictionary;

namespace UnityEditor.Sdon.I18N {

	public class XMLDictionaryService {

		private static XMLDictionaryService instance;

		public static XMLDictionaryService getInstance() {
			if (instance == null) {
				instance = new XMLDictionaryService();
			}
			return instance;
		}

		/// <summary>
		/// Читает словарь из XML файла в модель Dictionary
		/// </summary>
		/// <param name="xmlDictionary">XML файл словаря</param>
		/// <returns>Возвращает Dictionary модель</returns>
		public Dictionary ReadDictionary(string xmlDictionary) {

			string dictionaryName = Path.GetFileName(xmlDictionary);
			if (dictionaryName.ToLower().EndsWith(".xml")) {
				dictionaryName = dictionaryName.Substring(0,dictionaryName.Length-4);
			}

			Dictionary dictionary = new Dictionary(dictionaryName);

			XmlDocument doc = new XmlDocument();
			doc.Load(xmlDictionary);

			XmlNodeList langs = doc.GetElementsByTagName(LangXMLLoader.LOCALIZATION_NAME);
			foreach(XmlElement lang in langs) {
				dictionary.Langs.Add(ReadLang(lang));
			}
			
			return dictionary;
		}

		#region Read API
		/// <summary>
		/// Читает область языка
		/// </summary>
		/// <param name="langElement">XML элемент языка</param>
		private Lang ReadLang(XmlElement langElement) {

			string langName = langElement.GetAttributeNode(LangXMLLoader.LOCALIZATION_ATTRIBUTE).Value;
			
			Lang lang = new Lang(langName);

			foreach(XmlElement data in langElement.ChildNodes) {
				lang.Datas.Add(ReadData(data));
			}

			return lang;
		}

		/// <summary>
		/// Читает область данных
		/// </summary>
		/// <param name="dataElement">XML элемент области данных</param>
		private Data ReadData(XmlElement dataElement) {
			string dataName = dataElement.Name;
			Data data = new Data(dataName);

			foreach(XmlElement item in dataElement.ChildNodes) {
				if (item.Name == LangXMLLoader.ITEM_NAME) {
					data.Items.Add(ReadItem(item));
				} else {
					data.Datas.Add(ReadData(item));
				}
			}

			return data;
		}

		/// <summary>
		/// Читает слово
		/// </summary>
		/// <param name="itemElement">XML элемент слова</param>
		private Item ReadItem(XmlElement itemElement) {
			string id = itemElement.GetAttributeNode(LangXMLLoader.ITEM_ATTRIBUTE).Value;
			string value = itemElement.InnerText;
			return new Item(id,value);
		}
		#endregion

		/// <summary>
		/// Записывает словарь из модели Dictionary в XML файл
		/// </summary>
		/// <param name="dictionary">Исходная модель словаря</param>
		/// <param name="xmlOutDictionary">Выходной xml файл</param>
		public void WriteDictionary(Dictionary dictionary, string xmlOutDictionary) {

			XmlDocument doc = new XmlDocument();
			XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
			XmlElement data = doc.CreateElement(LangXMLLoader.ROOT_CONTAINER);
			doc.AppendChild(data);
			doc.InsertBefore(declaration, data);

				foreach(Lang lang in dictionary.Langs) {
					data.AppendChild(CreateLang(doc,lang));
				}
			
			doc.Save(xmlOutDictionary);

		}

		#region Write API

		private XmlElement CreateLang(XmlDocument doc, Lang lang) {
			XmlElement langElement = doc.CreateElement(LangXMLLoader.LOCALIZATION_NAME);
			langElement.SetAttribute(LangXMLLoader.LOCALIZATION_ATTRIBUTE, lang.Language);

			foreach (Data data in lang.Datas) {
				langElement.AppendChild(CreateData(doc, data));
			}

			return langElement;
		}

		private XmlElement CreateData(XmlDocument doc, Data data) {
			XmlElement dataElement = doc.CreateElement(data.Name);

			foreach(Data innerData in data.Datas) {
				dataElement.AppendChild(CreateData(doc,innerData));
			}

			foreach(Item item in data.Items) {
				dataElement.AppendChild(CreateItem(doc,item));
			}

			return dataElement;
		}

		private XmlElement CreateItem(XmlDocument doc, Item item) {
			XmlElement itemElement = doc.CreateElement(LangXMLLoader.ITEM_NAME);
				itemElement.SetAttribute(LangXMLLoader.ITEM_ATTRIBUTE,item.ID);
				itemElement.InnerText = item.Value;
			return itemElement;
		}

		#endregion

	}

}
