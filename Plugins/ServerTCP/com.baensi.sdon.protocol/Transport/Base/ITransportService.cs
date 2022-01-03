using com.baensi.sdon.protocol.entities;
using System.Collections.Generic;

namespace com.baensi.sdon.protocol.transport
{

    /// <summary>
    /// Интерфейс транспортного сервиса
    /// </summary>
    public interface ITransportService
    {

        /// <summary>
        /// Преобразует текстовые данные в транспортную сущность
        /// </summary>
        /// <typeparam name="T">Тип транспортной сущности</typeparam>
        /// <param name="data">Данные из которых необходимо получить сущность</param>
        /// <returns>Возвращает заполненный экземпляр транспортной сущности указанного типа</returns>
        T Get<T>(string data) where T : class, ITransportEntity;

        /// <summary>
        /// Преобразует множество текстовых данных в множество в транспортных сущностей
        /// </summary>
        /// <typeparam name="T">Тип транспортной сущности</typeparam>
        /// <param name="data">Данные из которых необходимо получить сущности</param>
        /// <returns>Возвращает множество заполненных экземпляров транспортных сущностей указанного типа</returns>
        IEnumerable<T> Get<T>(IEnumerable<string> data) where T : class, ITransportEntity;

        /// <summary>
        /// Преобразует транспортную сущность в данные
        /// </summary>
        /// <typeparam name="T">Тип транспортной сущности</typeparam>
        /// <param name="entity">Сущность, которую необходимо преобразовать в данные</param>
        /// <returns>Возвращает данные</returns>
        string Data<T>(T entity) where T : class, ITransportEntity;

        /// <summary>
        /// Преобразует множество транспортных сущностей в множество данных
        /// </summary>
        /// <typeparam name="T">Тип транспортной сущности</typeparam>
        /// <param name="entities">Множество сущностей, которые необходимо преобразовать во множества данных</param>
        /// <returns>Возвращает множество данных</returns>
        IEnumerable<string> Data<T>(IEnumerable<T> entities) where T : class, ITransportEntity;

    }

}
