using System;
using Engine.DB.I18n;
using System.Collections.Generic;
using Engine.DB;
using System.Linq;

namespace Engine
{

    public class Localization
    {

        #region Singleton

        private static readonly Lazy<Localization> instance = new Lazy<Localization>(() => new Localization());
        public static Localization Instance { get { return instance.Value; } }

        #endregion

        private LocalizationReader reader = new LocalizationReader();

        public Localization()
        {
            ReloadDictionary();
        }

        private ICollection<Lang> langs;

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

        private IDictionary<string, string> dictionary = null;


        public string Get(string key)
        {
            if (dictionary == null)
                return null;

            if (key == null)
                throw new ArgumentNullException("key can't be nullpointer!");

#if UNITY_EDITOR
            if (!dictionary.ContainsKey(key))
                throw new KeyNotFoundException("current language '" + CurrentLang.Name + "' with code '" + CurrentLang.Code + "' not contains value for key '" + key + "'!");
#endif

            return dictionary[key];
        }

        public bool ReloadDictionary()
        {
            var lang = CurrentLang;

            if (lang == null)
                return false;

            dictionary = reader.GetKeys(lang);
            return true;
        }

    }

}
