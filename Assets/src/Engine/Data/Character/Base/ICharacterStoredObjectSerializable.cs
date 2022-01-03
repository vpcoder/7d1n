
namespace Engine.Data
{

    /// <summary>
    /// Сериализуемый параметр парсонажа
    /// </summary>
    /// <typeparam name="T">Тип параметра персонажа</typeparam>
    public interface ICharacterStoredObjectSerializable<T> where T : class, IStoryObject
    {

        /// <summary>
        /// Формирует объек с данными для сериализации
        /// </summary>
        /// <returns>Объект с данными</returns>
        T CreateData();

        /// <summary>
        /// Читает данные из объекта сериализации
        /// </summary>
        /// <param name="data">Объект с данными</param>
        void LoadFromData(T data);

    }

}
