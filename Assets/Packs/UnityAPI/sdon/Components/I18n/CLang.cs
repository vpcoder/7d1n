using UnityEngine;
using System.Collections.Generic;
using Engine.Config;

namespace Engine.I18N {

	/// <summary>
	/// Класс-словарь, способный по keyword-у выдать фразу-перевод в текущей локализации. Локализация настраивается в классе GameConfig
	/// </summary>
	public class CLang {

		private List<string> localizations;
		private SortedDictionary<string,string> mapData;

		private static CLang instance;

		public static CLang getInstance(){
			if (instance == null)
				instance = new CLang();
			return instance;
		}

		// инициализация словаря
		public CLang() {

			mapData = new SortedDictionary<string,string>(); // инициализируем словарь
			localizations = new List<string>(); // инициализируем сисок локализаций

			foreach (TextAsset asset in Resources.LoadAll<TextAsset>(Dictionary.I18N_DIRECTORY_PATH)) { // перебираем все xml базы в папке со словорями
				ILangLoader loader = new LangXMLLoader(asset);
				loader.GetData(ref mapData, ref localizations); // записываем прочитанные данные в словарь
				loader = null;
			}

		}

		/// <summary>
		/// Возвращает логическое значение - содержит ли словарь указанный keyword **для ТЕКУЩЕЙ ЛОКАЛИЗАЦИИ**
		/// </summary>
		/// <returns>Логическое значение - содержит ли словарь указанный keyword **для ТЕКУЩЕЙ ЛОКАЛИЗАЦИИ**</returns>
		/// <param name="key">keyword, который надо проверить</param>
		public bool ContainsKey(string key) {
			return mapData.ContainsKey(GameConfig.Settings.GetValue(StrSettingsProprety.Localization) + key);
		}

		/// <summary>
		/// Возвращает фразу-перевод
		/// </summary>
		/// <param name="key">keyword для указанной фразы</param>
		public string get(string key) {
			
			string result   = "";
			string keyValue = GameConfig.Settings.GetValue(StrSettingsProprety.Localization) + key;

#if UNITY_EDITOR

			if(!ContainsKey(key)) {
				Debug.LogError("Попытка доступа к несуществующей надписи в словаре - '" + keyValue + "' не найден в словаре I18N!");
				return "";
			}

#endif

			mapData.TryGetValue(keyValue, out result);

			return result;

		}

#if UNITY_EDITOR

		/// <summary>
		/// Возвращает фразу-перевод (**Доступно ТОЛЬКО в редакторе!**) - используется в I18n-полях ввода
		/// </summary>
		/// <param name="key">keyword для указанной фразы</param>
		/// <param name="noError">Устанавливает игнорирование ошибочных значенией keywords</param>
		public string tryGet(string key, bool noError=true) {
			
			string result   = "";
			string keyValue = GameConfig.Settings.GetValue(StrSettingsProprety.Localization) + key;

			if(!noError && !ContainsKey(key)) {
				Debug.LogError("Попытка доступа к несуществующей надписи в словаре - '" + keyValue + "' не найден в словаре I18N!");
				return "";
			}
				
			if (mapData.ContainsKey(keyValue)) {
				mapData.TryGetValue(keyValue, out result);
			}

			return result;

		}

#endif

	}
}