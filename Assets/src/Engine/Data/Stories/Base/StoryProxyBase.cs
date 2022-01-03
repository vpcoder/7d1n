
namespace Engine.Data.Stories
{

    /// <summary>
    /// Базовый класс прокси
    /// </summary>
    /// <typeparam name="T">Тип объекта в хранилище</typeparam>
    public abstract class StoryProxyBase<T> : IStoryProxy<T> where T : class, IStoryObject
    {

        /// <summary>
        /// Удаляет объект из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        public abstract void Delete(long id);

        /// <summary>
        /// Проверяет, существует ли объект с указанным идентификатором в хранилище
        /// </summary>
        /// <param name="id">Идентификатор проверяемого на существование объекта</param>
        /// <returns>Возвращает логическое значение "существует ли указанный объект в хранилище?"</returns>
        public abstract bool Exists(long id);

        /// <summary>
        /// Достаёт предмет из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор доставаемого предмета</param>
        /// <returns>Возвращает объект из хранилища</returns>
        public abstract T Get(long id);

        /// <summary>
        /// Умное сохранение объекта в хранилище.
        /// Если объект уже существовал, то обновятся его параметры в хранилище.
        /// Если его не было, то добавится объект в хранилище.
        /// </summary>
        /// <param name="storyObject">Сохраняемый объект</param>
        public abstract void Save(T storyObject);

    }

}
