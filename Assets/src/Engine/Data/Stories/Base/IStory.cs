
namespace Engine.Data.Stories
{

    /// <summary>
    /// Хранилище
    /// </summary>
    public interface IStory
    {

        /// <summary>
        /// Удаляет объект из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        void Delete(long id);

        /// <summary>
        /// Проверяет, существует ли объект с указанным идентификатором в хранилище
        /// </summary>
        /// <param name="id">Идентификатор проверяемого на существование объекта</param>
        /// <returns>Возвращает логическое значение "существует ли указанный объект в хранилище?"</returns>
        bool Exists(long id);

    }

    /// <summary>
    /// Типизированное хранилище
    /// </summary>
    /// <typeparam name="T">Тип объекта в хранилище</typeparam>
    public interface IStory<T> : IStory where T : class, IStoryObject
    {

        /// <summary>
        /// Прокси хранилища
        /// </summary>
        IStoryProxy<T> Proxy { get; }

        /// <summary>
        /// Достаёт предмет из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор доставаемого предмета</param>
        /// <returns>Возвращает объект из хранилища</returns>
        T Get(long id);

        /// <summary>
        /// Удаляет объект из хранилища
        /// </summary>
        /// <param name="storyObject">Удаляемый объект</param>
        void Delete(T storyObject);

        /// <summary>
        /// Умное сохранение объекта в хранилище.
        /// Если объект уже существовал, то обновятся его параметры в хранилище.
        /// Если его не было, то добавится объект в хранилище.
        /// </summary>
        /// <param name="storyObject">Сохраняемый объект</param>
        void Save(T storyObject);

    }

}
