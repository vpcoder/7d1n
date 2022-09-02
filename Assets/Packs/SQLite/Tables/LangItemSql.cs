using System;
using Engine.I18n;

namespace Engine.DB.I18n
{

    /// <summary>
    /// 
    /// Экземпляр языка
    /// На каждый язык создаётся такой экземпляр
    /// ---
    /// A language instance
    /// For each language such an instance is created
    /// 
    /// </summary>
    [Serializable]
    [Table("i18n_lang")] // Известный язык из базы (русский, английский и т.д.)
    public class LangItemSql : Dto, ILangItem
    {

        /// <summary>
        ///     Код языка (ru_ru, en_us, и т.д.)
        ///     ---
        ///     Language code (ru_ru, en_us, etc.)
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        ///     Название языка в локализованном виде, например "Русский", "English" и т.д.
        ///     ---
        ///     Language name in localized form, e.g. "Русский", "English", etc.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

    }

}
