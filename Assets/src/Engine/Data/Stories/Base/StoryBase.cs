
using System;

namespace Engine.Data.Stories
{

    /// <summary>
    /// Базовый класс хранилища
    /// </summary>
    /// <typeparam name="T">Тип объекта, хранимого в хранилище</typeparam>
    public abstract class StoryBase<T, TProxy> : IStory<T> where T : class, IStoryObject
                                                           where TProxy : class, IStoryProxy<T>
    {

        public StoryBase()
        {
            this.Proxy = Activator.CreateInstance<TProxy>();
        }

        /// <summary>
        /// Прокси хранилища
        /// </summary>
        public IStoryProxy<T> Proxy { get; }

        /// <summary>
        /// Удаляет объект из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        public virtual void Delete(long id)
        {
            this.Proxy.Delete(id);
        }

        /// <summary>
        /// Удаляет объект из хранилища
        /// </summary>
        /// <param name="storyObject">Удаляемый объект</param>
        public virtual void Delete(T storyObject)
        {
            this.Delete(storyObject.ID);
        }

        /// <summary>
        /// Проверяет, существует ли объект с указанным идентификатором в хранилище
        /// </summary>
        /// <param name="id">Идентификатор проверяемого на существование объекта</param>
        /// <returns>Возвращает логическое значение "существует ли указанный объект в хранилище?"</returns>
        public virtual bool Exists(long id)
        {
            return this.Proxy.Exists(id);
        }

        /// <summary>
        /// Достаёт предмет из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор доставаемого предмета</param>
        /// <returns>Возвращает объект из хранилища</returns>
        public virtual T Get(long id)
        {
            return this.Proxy.Get(id);
        }

        /// <summary>
        /// Умное сохранение объекта в хранилище.
        /// Если объект уже существовал, то обновятся его параметры в хранилище.
        /// Если его не было, то добавится объект в хранилище.
        /// </summary>
        /// <param name="storyObject">Сохраняемый объект</param>
        public virtual void Save(T storyObject)
        {
            this.Proxy.Save(storyObject);
        }

    }

}
