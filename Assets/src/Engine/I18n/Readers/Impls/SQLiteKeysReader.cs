using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.DB.I18n
{

    /// <summary>
    ///
    /// Загрузчик словарей из SQLite
    /// ---
    /// Loader of dictionaries from SQLite
    /// 
    /// </summary>
    public class SQLiteKeysReader : ILocalizationKeysReader
    {

        /// <summary>
        ///     Метод должен предоставить мапу фраз-переводов по переданному словарю
        ///     ---
        ///     The method should provide a mapping of phrase-translations from the passed vocabulary
        /// </summary>
        /// <param name="lang">
        ///     Словарь по которому необходимо получить мапу доступных фраз-переводов
        ///     ---
        ///     Dictionary by which you want to get a map of available phrase translations
        /// </param>
        /// <returns>
        ///     Мапу вида Ключ->Перевод
        ///     ---
        ///     Key->Translate Map
        /// </returns>
        public IDictionary<string, string> GetKeys(Lang lang)
        {
            IDictionary<string, string> values = new Dictionary<string, string>();

            foreach (var item in ReadKeys(lang.Code))
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    throw new NullReferenceException("table with language '" + lang.Name + "' with code '" + lang.Code + "' contains empty key!");
                }

#if UNITY_EDITOR
                if (string.IsNullOrEmpty(item.Value))
                {

                    Debug.LogWarning("table with language '" + lang.Name + "' with code '" + lang.Code + "' contains empty value by key '" + item.Key + "'!");

                }
#endif

                values.Add(item.Key, item.Value);
            }

            return values;
        }

        private ICollection<LangKey> ReadKeys(string code)
        {
            string tableName = "i18n_" + code;
            IList<LangKey> keys = null;
            Db.Instance.Do(connection =>
            {
                keys = connection.Query<LangKey>("SELECT * FROM " + tableName + ";");
            });
            return keys;
        }

        /// <summary>
        ///     Метод должен загрузить коллекцию доступных словарей (каждый словарь описывает локализацию конкретного языка)
        ///     ---
        ///     The method should load a collection of available dictionaries (each dictionary describes the localization of a particular language)
        /// </summary>
        /// <returns>
        ///     Коллекцию загруженных словарей
        ///     ---
        ///     Collection of loaded dictionaries
        /// </returns>
        public ICollection<Lang> GetAllLangs()
        {
            IList<Lang> langs = null;

            Db.Instance.Do(connection =>
            {
                langs = connection.QueryAll<Lang>();
            });

            return langs;
        }

    }

}
