
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Объект с идентификатором
    /// ---
    /// Object with identifier
    /// 
    /// </summary>
    public interface IIdentity
    {

        ///<summary>
        ///     Идентификатор
        ///     ---
        ///     Identifier
        ///</summary>
        long ID { get; set; }

        /// <summary>
        ///     Копирует текущую сущность в новый экземпляр
        ///     ---
        ///     Copies the current entity into a new instance
        /// </summary>
        /// <returns>
        ///     Копия сущности
        ///     ---
        ///     Entity Copy
        /// </returns>
        IIdentity Copy();

    }

}
