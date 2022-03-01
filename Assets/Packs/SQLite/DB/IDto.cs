
namespace Engine.DB
{

    /// <summary>
    /// 
    /// Объект БД имеющий идентификатор
    /// ---
    /// A database object that has an identifier
    /// 
    /// </summary>
    public interface IDto
    {

        ///<summary>
        ///     Идентификатор объекта
        ///     Обязан быть уникален в рамках своего типа
        ///     ----
        ///     Object identifier
        ///     Must be unique within its type
        ///</summary>
        long ID { get; set; }

    }

}
