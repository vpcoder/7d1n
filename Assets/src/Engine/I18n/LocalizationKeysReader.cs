using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.DB.I18n
{

    /// <summary>
    /// 
    /// </summary>
    public class LocalizationReader
    {

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
