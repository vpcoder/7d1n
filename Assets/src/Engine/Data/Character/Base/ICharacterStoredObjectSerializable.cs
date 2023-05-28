
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Сериализуемый параметр парсонажа.
    /// Необходим для хранения параметров в хранилищах.
    /// ---
    /// A serializable parson parameter.
    /// Necessary for storing parameters in storages.
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип параметра персонажа, которых необходимо хранить
    ///     ---
    ///     Type of character parameter to be stored
    /// </typeparam>
    public interface ICharacterStoredObjectSerializable<T> where T : class, IRepositoryObject
    {

        /// <summary>
        ///     Формирует объект с данными для сериализации, необходимый для сохранения в хранилище
        ///     ---
        ///     Creates an object with data for serialization, which is necessary to save in the repository
        /// </summary>
        /// <returns>
        ///     Объект с данными
        ///     ---
        ///     Data object
        /// </returns>
        T CreateData();

        /// <summary>
        ///     Читает данные из объекта сериализации, необходимо для чтения объекта из хранилища
        ///     ---
        ///     Reads data from the serialization object, necessary to read the object from the repository
        /// </summary>
        /// <param name="data">
        ///     Объект с данными
        ///     ---
        ///     Data object
        /// </param>
        void LoadFromData(T data);

    }

}
