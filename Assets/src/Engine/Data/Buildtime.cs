using System;

namespace Engine.Data
{

    /// <summary>
    ///
    /// Информация о сборке проекта 7d1n
    /// ---
    /// Project build information 7d1n
    /// 
    /// </summary>
    [Serializable]
    public class Buildtime
    {
        /// <summary>
        ///     Текущая версия продукта, влияет на версию БД
        ///     ---
        ///     Current version of the product, affects the version of the database
        /// </summary>
        public Version Version { get; } = new Version("1.0.0.53");

    }

}
