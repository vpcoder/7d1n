using System;
using Engine.DB.I18n;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine
{

    /// <summary>
    /// 
    /// Фабрика хранящая словари локализации.
    /// Позволяет получать локализованные строки по ключам.
    /// ---
    /// Factory storing localization dictionaries.
    /// Allows to get localized strings by keys.
    /// 
    /// </summary>
    public class Localization
    {

        #region Singleton
        private static readonly Lazy<Localization> instance = new Lazy<Localization>(() => new Localization());
        public static Localization Instance { get { return instance.Value; } }
        private Localization()
        {
            ReloadDictionary();
        }
        #endregion

        #region Hidden Fields

        /// <summary>
        ///     Загрузчик словарей выбранного языка
        ///     ---
        ///     Reader of dictionaries of the selected language
        /// </summary>
        private ILocalizationKeysReader reader = new SQLiteKeysReader();

        /// <summary>
        ///     Словарь текущего выбранного языка в виде "Ключ" -> "Локализованная строка"
        ///     ---
        ///     Dictionary of the currently selected language in the form of "Key" -> "Localized string"
        /// </summary>
        private IDictionary<string, string> dictionary;

        /// <summary>
        ///     Список доступных языков
        ///     ---
        ///     List of available languages
        /// </summary>
        private ICollection<Lang> langs;

        #endregion

        #region Properties

        public const string DEFAULT_LANG_CODE = "en_us";
        
        /// <summary>
        ///     Читает и возвращает список доступных языков из БД. Свойство кешировано.
        ///     ---
        ///     Reads and returns the list of available languages from the database. The property is cached.
        /// </summary>
        public ICollection<Lang> Langs
        {
            get
            {
                if (langs == null)
                {
                    langs = reader.GetAllLangs();
                }
                return langs;
            }
        }

        /// <summary>
        ///     Пытается определить язык системы, и по нему начитать язык игры
        ///     Если в процессе определения языка не удалось определить iso код, используется язык по умолчанию
        ///     ---
        ///     It tries to detect the language of the system and use it to read the game language.
        ///     If no iso code is detected during the language detection process, the default language is used
        /// </summary>
        public Lang TryGetSystemLangOrDefault
        {
            get
            {
                var isoCode = GetIsoCode(Application.systemLanguage);
                if (isoCode == null)
                    isoCode = DEFAULT_LANG_CODE;
                return GetByIsoCode(isoCode);
            }
        }
        
        /// <summary>
        ///     Читает и возвращает текущий выбранный язык в настройках хранящихся в БД
        ///     ---
        ///     Reads and returns the currently selected language in the settings stored in the database
        /// </summary>
        public Lang CurrentLang
        {
            get
            {
                var langCode = GameSettings.Instance.Settings.Language;
                if (langCode == null)
                    return TryGetSystemLangOrDefault;
                return GetByIsoCode(langCode);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Ищет среди доступных языков в игре словарь по iso коду
        ///     ---
        ///     Searches among the available languages in the game dictionary by iso code
        /// </summary>
        /// <param name="isoCode">
        ///     ISO код языка, по которому нужно найти словарь
        ///     ---
        ///     ISO code of the language for which you want to find the dictionary
        /// </param>
        /// <returns>
        ///     Словарь найденный по iso коду, или словарь по умолчанию, если не удалось найти подходящего
        ///     ---
        ///     Dictionary found by iso code, or the default dictionary if no suitable one could be found
        /// </returns>
        private Lang GetByIsoCode(string isoCode)
        {
            return Langs.FirstOrDefault(o => o.Code == isoCode);
        }
        
        /// <summary>
        ///     Преобразует енум системного языка в iso код языка
        ///     ---
        ///     Converts the system language enum into iso language code
        /// </summary>
        /// <param name="language">
        ///     Язык в системе
        ///     ---
        ///     Language in the system
        /// </param>
        /// <returns>
        ///     ISO код языка
        ///     ---
        ///     ISO language code
        /// </returns>
        private string GetIsoCode(SystemLanguage language)
        {
            switch (language)
            {
                case SystemLanguage.Afrikaans: return "af_za";
                case SystemLanguage.Arabic: return "ar_ae";
                case SystemLanguage.Basque: return "eu_es";
                case SystemLanguage.Belarusian: return "be_by";
                case SystemLanguage.Bulgarian: return "bg_bg";
                case SystemLanguage.Catalan: return "ca_es";
                case SystemLanguage.Chinese: return "zh_cn";
                case SystemLanguage.ChineseSimplified: return "zh_cn";
                case SystemLanguage.ChineseTraditional: return "zh_cn";
                case SystemLanguage.Czech: return "cs_cz";
                case SystemLanguage.Danish: return "da_dk";
                case SystemLanguage.Dutch: return "nl_be";
                case SystemLanguage.English: return "en_us";
                case SystemLanguage.Estonian: return "et_ee";
                case SystemLanguage.Faroese: return "fo_fo";
                case SystemLanguage.Finnish: return "fi_fi";
                case SystemLanguage.French: return "fr_fr";
                case SystemLanguage.German: return "de_de";
                case SystemLanguage.Greek: return "el_gr";
                case SystemLanguage.Hebrew: return "he_il";
                case SystemLanguage.Hungarian: return "hu_hu";
                case SystemLanguage.Icelandic: return "is_is";
                case SystemLanguage.Indonesian: return "id_id";
                case SystemLanguage.Italian: return "it_it";
                case SystemLanguage.Japanese: return "ja_jp";
                case SystemLanguage.Korean: return "ko_kr";
                case SystemLanguage.Latvian: return "lv_lv";
                case SystemLanguage.Lithuanian: return "lt_lt";
                case SystemLanguage.Norwegian: return "nb_no";
                case SystemLanguage.Polish: return "pl_pl";
                case SystemLanguage.Portuguese: return "pt_pt";
                case SystemLanguage.Romanian: return "ro_ro";
                case SystemLanguage.Russian: return "ru_ru";
                case SystemLanguage.Slovak: return "sk_sk";
                case SystemLanguage.Slovenian: return "sl_sl";
                case SystemLanguage.Spanish: return "es_es";
                case SystemLanguage.Swedish: return "sv_se";
                case SystemLanguage.SerboCroatian: return "hr_hr";
                case SystemLanguage.Thai: return "th_th";
                case SystemLanguage.Turkish: return "tr_tr";
                case SystemLanguage.Ukrainian: return "uk_ua";
                case SystemLanguage.Vietnamese: return "vi_vn";
                default:
                    return null;
            }
        }
        
        /// <summary>
        ///     Возвращает локализованную строку по ключу.
        ///     Подразумевается что ранее уже был выбран язык.
        ///     ---
        ///     Returns localized string by key.
        ///     It is implied that the language has already been selected.
        /// </summary>
        /// <param name="key">
        ///     Ключ с которым ассоциирована локализованная строка
        ///     ---
        ///     The key with which the localized string is associated
        /// </param>
        /// <returns>
        ///     Локализованная строка на выбранном в настройке GameSettings.Instance.Settings.Language языке
        ///     ---
        ///     The localized string in the language selected in the GameSettings.Instance.Settings.Language setting
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Ключ не может быть null
        ///     ---
        ///     The key cannot be null
        /// </exception>
        public string Get(string key)
        {
            if (dictionary == null)
                ReloadDictionary();

#if UNITY_EDITOR && DEBUG
            if (key == null)
                throw new ArgumentNullException("key can't be nullpointer!");
#endif

            if (!dictionary.TryGetValue(key, out var message))
            {
                message = "?";
                Debug.LogError("current language '" + CurrentLang.Name + "' with code '" + CurrentLang.Code + "' not contains value for key '" + key + "'!");
            }

            return message;
        }

#if UNITY_EDITOR
        
        public string GetUnsafe(string key)
        {
            if (dictionary == null)
                ReloadDictionary();
            dictionary.TryGetValue(key, out var message);
            return message;
        }
        
#endif

        /// <summary>
        ///     Выполняет загрузку ключей по выбранному языку из БД в кеш локализации
        ///     ---
        ///     Loads keys for the selected language from the database into the localization cache
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///     Выбранный в настройке язык не найден в БД по индексу. Возможно БД повреждена.
        ///     ---
        ///     The language selected in the configuration is not found in the database by index. Possibly the database is corrupted.
        /// </exception>
        public void ReloadDictionary()
        {
            var lang = CurrentLang;
            if (lang == null)
                throw new NotSupportedException("Selected lang code '" + GameSettings.Instance.Settings.Language + "' isn't supported! Database is corrupted?");

            dictionary = reader.GetKeys(lang);
        }

        #endregion

    }

}
