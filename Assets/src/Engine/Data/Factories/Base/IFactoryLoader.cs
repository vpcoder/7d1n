using System.Collections.Generic;

namespace Engine.Data.Factories
{

    /// <summary>
    /// 
    /// Загрузчик фабрики
    /// Параметризованный загрузчик, выполняет загрузку указанного типа сущностей
    /// ---
    /// Factory loader
    /// Parametrized loader, performs loading of a specified type of entities
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип объекта который будет загружаться
    ///     ---
    ///     The type of object to be loaded
    /// </typeparam>
    public interface IFactoryLoader<T> where T : class, IIdentity
    {

        /// <summary>
        ///     Выполняет загрузку всех объектов нужного типа для фабрики
        ///     ---
        ///     Loads all objects of the right type for the factory
        /// </summary>
        /// <returns>
        ///     Возвращает коллекцию загруженных объектов
        ///     ---
        ///     Returns a collection of loaded objects
        /// </returns>
        ICollection<T> Load();

    }

}
