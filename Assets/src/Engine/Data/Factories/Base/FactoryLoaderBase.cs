using System.Collections.Generic;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Базовый класс загрузчика фабрики
    /// </summary>
    /// <typeparam name="T">Тип хранимого объекта в фабрике</typeparam>
    public abstract class FactoryLoaderBase<T> : IFactoryLoader<T> where T : class, IIdentity
    {

        /// <summary>
        /// Выполняет загрузку всех объектов для фабрики
        /// </summary>
        /// <returns>Возвращает коллекцию загруженных объектов</returns>
        public abstract ICollection<T> Load();

    }

}
