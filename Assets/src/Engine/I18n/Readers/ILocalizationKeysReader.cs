using System.Collections.Generic;

namespace Engine.DB.I18n
{
    
    /// <summary>
    ///
    /// Базовый загрузчик словарей
    /// ---
    /// Basic dictionary loader
    /// 
    /// </summary>
    public interface ILocalizationKeysReader
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
        IDictionary<string, string> GetKeys(Lang lang);

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
        ICollection<Lang> GetAllLangs();

    }
    
}
