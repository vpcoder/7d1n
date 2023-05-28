using Engine.Collections;
using Engine.DB;
using System;

namespace Engine.Data.Repositories
{

    /// <summary>
    ///
    /// Прокси для хранилища на основе SQLite
    /// ---
    /// Proxy for SQLite-based storage
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип объекта в хранилище
    ///     ---
    ///     Type of object in the storage
    /// </typeparam>
    public abstract class SqliteRepositoryProxyBase<T, TSqlProxy> : RepositoryProxyBase<T> where T : Dto, IRepositoryObject, new()
                                                                                 where TSqlProxy : class, IDbCollection<T>
    {

        /// <summary>
        ///     Ленивый конструктор проксирующей коллекции
        ///     ---
        ///     Lazy proxy collection builder
        /// </summary>
        private Lazy<TSqlProxy> ProxyInstance = new Lazy<TSqlProxy>(() => Activator.CreateInstance<TSqlProxy>());

        /// <summary>
        ///     Проксирующая коллекция для работы с БД
        ///     ---
        ///     Proxying collection to work with the database
        /// </summary>
        private TSqlProxy Proxy => ProxyInstance.Value;
        
        /// <summary>
        ///     Удаляет объект из хранилища по идентификатору
        ///     ---
        ///     Deletes the object from the storage by identifier
        /// </summary>
        /// <param name="id">
        ///     Идентификатор удаляемого объекта
        ///     ---
        ///     Identifier of the object to be deleted
        /// </param>
        public override void Delete(long id)
        {
            Proxy.Delete(id);
        }

        /// <summary>
        ///     Проверяет, существует ли объект с указанным идентификатором в хранилище
        ///     ---
        ///     Checks if an object with the specified identifier exists in the repository
        /// </summary>
        /// <param name="id">
        ///     Идентификатор проверяемого на существование объекта
        ///     ---
        ///     Identifier of the object checked for existence
        /// </param>
        /// <returns>
        ///     Возвращает логическое значение "существует ли указанный объект в хранилище?"
        ///     ---
        ///     Returns the boolean value "Does the specified object exist in the repository?"
        /// </returns>
        public override bool Exists(long id)
        {
            return Proxy.IsExists(id);
        }

        /// <summary>
        ///     Достаёт предмет из хранилища по идентификатору
        ///     ---
        ///     Retrieves an item from the vault by ID
        /// </summary>
        /// <param name="id">
        ///     Идентификатор доставаемого предмета
        ///     ---
        ///     The identifier of the retrievable item
        /// </param>
        /// <returns>
        ///     Возвращает объект из хранилища
        ///     ---
        ///     Returns the object from the repository
        /// </returns>
        public override T Get(long id)
        {
            return Proxy.Get(id);
        }

        /// <summary>
        ///     Умное сохранение объекта в хранилище.
        ///     Если объект уже существовал, то обновятся его параметры в хранилище.
        ///     Если его не было, то добавится объект в хранилище.
        ///     ---
        ///     Smart save object to the repository.
        ///     If the object already existed, its parameters will be updated in the repository.
        ///     If it didn't exist, the object will be added to the repository.
        /// </summary>
        /// <param name="repositoryObject">
        ///     Сохраняемый объект
        ///     ---
        ///     A retained object
        /// </param>
        public override void Save(T repositoryObject)
        {
            Proxy.Save(repositoryObject);
        }

    }

}
