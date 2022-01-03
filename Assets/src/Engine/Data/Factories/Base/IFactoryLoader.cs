using System.Collections.Generic;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Загрузчик фабрики
    /// </summary>
    /// <typeparam name="T">Тип объекта, хранимого в фабрике</typeparam>
    public interface IFactoryLoader<T> where T : class, IIdentity
    {

        /// <summary>
        /// Выполняет загрузку всех объектов для фабрики
        /// </summary>
        /// <returns>Возвращает коллекцию загруженных объектов</returns>
        ICollection<T> Load();

    }

}
