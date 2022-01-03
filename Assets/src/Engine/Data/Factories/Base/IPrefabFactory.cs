
namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика префабов
    /// </summary>
    /// <typeparam name="T">Тип хранимого объекта в фабрике</typeparam>
    public interface IPrefabFactory<T>
    {

        /// <summary>
        /// Адрес дирректории, где находятся префабы
        /// </summary>
        string Directory { get; }

        /// <summary>
        /// Возвращает Экземпляр префаба по его текстовому идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Загруженный (кешированный) экземпляр префаба</returns>
        T Get(string id);

    }

}
