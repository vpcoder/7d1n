using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using Engine.Config;

namespace Engine.I18N {

	/// <summary>
	/// Класс-загрузчик языковых данных в словарь
	/// </summary>
	public class LangXMLLoader : ILangLoader {

		#region Constants Fields

		/// <summary>
		/// Корневой контейнер, внутри которого расположены все XML данные
		/// </summary>
		public const string ROOT_CONTAINER         = "data";

		/// <summary>
		/// Контейнер, внутри которого расположены фразы конкретной локализации
		/// </summary>
		public const string LOCALIZATION_NAME      = "lang";

		/// <summary>
		/// Аттрибут контейнера LOCALIZATION_NAME, который отвечает за название локализации
		/// </summary>
		public const string LOCALIZATION_ATTRIBUTE = "name";

		/// <summary>
		/// Контейнер, содержимое которого является - фраза-перевод
		/// </summary>
		public const string ITEM_NAME              = "item";

		/// <summary>
		/// Аттрибут контейнера ITEM_NAME, значением которого является keyword для фразы-перевода
		/// </summary>
		public const string ITEM_ATTRIBUTE         = "id";

		#endregion


		#region Hiden Field

		/// <summary>
		/// Поле с текстовым ресурсом (сам ресурс лежит в папке Resources, не зависимо от платформы (в том числе Android))
		/// </summary>
		private TextAsset asset;

		#endregion


		public LangXMLLoader(TextAsset asset){
			this.asset = asset;
		}


		#region Methods

		/// <summary>
		/// Заполняет словарь data и список локализаций localizations
		/// </summary>
		/// <param name="data">Словарь с парами - keyword-перевод</param>
		/// <param name="localizations">Список доступных локализаций</param>
		public void GetData(ref SortedDictionary<string,string> data, ref List<string> localizations){
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(asset.text);

			string currentLocal = GameConfig.Settings.GetValue(StrSettingsProprety.Localization);
			string key;
			string value;

			XmlNodeList langs = doc.GetElementsByTagName(LOCALIZATION_NAME);

			foreach(XmlElement lang in langs) {

				currentLocal = lang.GetAttribute(LOCALIZATION_ATTRIBUTE);

				if (!localizations.Contains(currentLocal)) {
					localizations.Add(currentLocal);
				}

				XmlNodeList items = lang.GetElementsByTagName(ITEM_NAME);

				foreach(XmlElement item in items) {

					key   = item.GetAttribute(ITEM_ATTRIBUTE);
					value = item.InnerText;

					if (key != null) {
						data.Add(currentLocal + key, value);
					}

				}

			}

			doc = null;

		}

		#endregion

	}

}
