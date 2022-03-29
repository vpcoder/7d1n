using Engine.Collections;
using Engine.DB;
using System;

namespace Engine.Data.Stories
{

    /// <summary>
    /// Прокси для хранилища на основе SQLite
    /// </summary>
    /// <typeparam name="T">Тип объекта в хранилище</typeparam>
    public abstract class SqliteStoryProxyBase<T, TSqlProxy> : StoryProxyBase<T> where T : Dto, IStoryObject, new()
                                                                                 where TSqlProxy : class, IDbCollection<T>
    {

        /// <summary>
        ///     Ленивый конструктор проксирующей коллекции
        /// </summary>
        private Lazy<TSqlProxy> ProxyInstance = new Lazy<TSqlProxy>(() => Activator.CreateInstance<TSqlProxy>());

        /// <summary>
        ///     Проксирующая коллекция для работы с БД
        /// </summary>
        private TSqlProxy Proxy => ProxyInstance.Value;
        
        /// <summary>
        /// Удаляет объект из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        public override void Delete(long id)
        {
            Proxy.Delete(id);
        }

        /// <summary>
        /// Проверяет, существует ли объект с указанным идентификатором в хранилище
        /// </summary>
        /// <param name="id">Идентификатор проверяемого на существование объекта</param>
        /// <returns>Возвращает логическое значение "существует ли указанный объект в хранилище?"</returns>
        public override bool Exists(long id)
        {
            return Proxy.IsExists(id);
        }

        /// <summary>
        /// Достаёт предмет из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор доставаемого предмета</param>
        /// <returns>Возвращает объект из хранилища</returns>
        public override T Get(long id)
        {
            return Proxy.Get(id);
        }

        /// <summary>
        /// Умное сохранение объекта в хранилище.
        /// Если объект уже существовал, то обновятся его параметры в хранилище.
        /// Если его не было, то добавится объект в хранилище.
        /// </summary>
        /// <param name="storyObject">Сохраняемый объект</param>
        public override void Save(T storyObject)
        {
            Proxy.Save(storyObject);
        }

    }

}
