
using System;

namespace Engine.Data.Repositories
{

    /// <summary>
    /// 
    /// Базовый класс хранилища
    /// ---
    /// Repository base class
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип объекта, хранимого в хранилище
    ///     ---
    ///     Type of object stored in the repository
    /// </typeparam>
    public abstract class RepositoryBase<T, TProxy> : IRepository<T> where T : class, IRepositoryObject
                                                           where TProxy : class, IRepositoryProxy<T>
    {

        public RepositoryBase()
        {
            this.Proxy = Activator.CreateInstance<TProxy>();
        }

        /// <summary>
        ///     Прокси хранилища
        ///     ---
        ///     Proxy repository
        /// </summary>
        public IRepositoryProxy<T> Proxy { get; }

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
        public virtual void Delete(long id)
        {
            this.Proxy.Delete(id);
        }

        /// <summary>
        ///     Удаляет объект из хранилища
        ///     ---
        ///     Deletes the object from the repository
        /// </summary>
        /// <param name="repositoryObject">
        ///     Удаляемый объект
        ///     ---
        ///     Removable object
        /// </param>
        public virtual void Delete(T repositoryObject)
        {
            this.Delete(repositoryObject.ID);
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
        public virtual bool Exists(long id)
        {
            return this.Proxy.Exists(id);
        }

        /// <summary>
        ///     Достаёт объект из хранилища по идентификатору
        ///     ---
        ///     Retrieves an object from the vault by ID
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
        public virtual T Get(long id)
        {
            return this.Proxy.Get(id);
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
        public virtual void Save(T repositoryObject)
        {
            this.Proxy.Save(repositoryObject);
        }

    }

}
