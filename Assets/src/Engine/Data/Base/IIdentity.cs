
namespace Engine.Data
{

    /// <summary>
    /// Объект с идентификатором
    /// </summary>
    public interface IIdentity
    {

        ///<summary>
        /// Идентификатор
        ///</summary>
        long ID { get; set; }

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        IIdentity Copy();

    }

}
