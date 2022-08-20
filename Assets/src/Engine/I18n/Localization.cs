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
        /// Загрузчик словарей выбранного языка
        /// ---
        /// Reader of dictionaries of the selected language
        /// </summary>
        private LocalizationReader reader = new LocalizationReader();

        /// <summary>
        /// Словарь текущего выбранного языка в виде "Ключ" -> "Локализованная строка"
        /// ---
        /// Dictionary of the currently selected language in the form of "Key" -> "Localized string"
        /// </summary>
        private IDictionary<string, string> dictionary = null;

        /// <summary>
        /// Список доступных языков
        /// ---
        /// List of available languages
        /// </summary>
        private ICollection<Lang> langs;

        #endregion

        #region Properties

        /// <summary>
        /// Читает и возвращает список доступных языков из БД. Свойство кешировано.
        /// ---
        /// Reads and returns the list of available languages from the database. The property is cached.
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
        /// Читает и возвращает текущий выбранный язык в настройках хранящихся в БД
        /// ---
        /// Reads and returns the currently selected language in the settings stored in the database
        /// </summary>
        public Lang CurrentLang
        {
            get
            {
                var langId = GameSettings.Instance.Settings.Language;

                if (langId < 0)
                    return null;

                return Langs.FirstOrDefault(o => o.ID == langId);
            }
        }

        #endregion

        #region Methods

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

        public string GetUnsafe(string key)
        {
            if (dictionary == null)
                ReloadDictionary();
            dictionary.TryGetValue(key, out var message);
            return message;
        }

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
                throw new NotSupportedException("Selected lang id '" + GameSettings.Instance.Settings.Language + "' isn't supported! Database is corrupted?");

            dictionary = reader.GetKeys(lang);
        }

        #endregion

    }

}
