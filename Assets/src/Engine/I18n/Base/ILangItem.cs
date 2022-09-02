namespace Engine.I18n
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
    public interface ILangItem
    {
        /// <summary>
        ///     Код языка (ru_ru, en_us, и т.д.)
        ///     ---
        ///     Language code (ru_ru, en_us, etc.)
        /// </summary>
        string Code { get; }

        /// <summary>
        ///     Название языка в локализованном виде, например "Русский", "English" и т.д.
        ///     ---
        ///     Language name in localized form, e.g. "Русский", "English", etc.
        /// </summary>
        string Name { get; }
        
    }
}