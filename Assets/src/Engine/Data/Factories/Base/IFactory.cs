using System.Collections.Generic;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика
    /// </summary>
    public interface IFactory
    {

        /// <summary>
        /// Выполняет перезагрузку фабрики
        /// </summary>
        void ReloadFactory();

    }

    /// <summary>
    /// Параметризированная фабрика
    /// </summary>
    /// <typeparam name="T">Тип объекта, хранящегося в фабрике</typeparam>
    public interface IFactory<T> : IFactory where T : class, IIdentity
    {

        /// <summary>
        /// Возвращает весь словарь фабрики
        /// </summary>
        IDictionary<long, T> Data { get; }

        /// <summary>
        /// Возвращает загрузчика фабрики
        /// </summary>
        IFactoryLoader<T> Loader { get; }

        /// <summary>
        /// Возвращает объект по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns></returns>
        T Get(long id);

        /// <summary>
        /// Создаёт новый экземпляр объекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор обхекта, который надо создать</param>
        /// <returns>Возвращает новый экземпляр объекта</returns>
        T Create(long id);

    }

}
