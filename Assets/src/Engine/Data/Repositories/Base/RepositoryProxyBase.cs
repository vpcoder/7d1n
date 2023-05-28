﻿
namespace Engine.Data.Repositories
{

    /// <summary>
    ///
    /// Базовый класс прокси
    /// ---
    /// Basic proxy class
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип объекта в хранилище
    ///     ---
    ///     Type of object in the storage
    /// </typeparam>
    public abstract class RepositoryProxyBase<T> : IRepositoryProxy<T> where T : class, IRepositoryObject
    {

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
        public abstract void Delete(long id);

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
        public abstract bool Exists(long id);

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
        public abstract T Get(long id);

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
        public abstract void Save(T repositoryObject);

    }

}
