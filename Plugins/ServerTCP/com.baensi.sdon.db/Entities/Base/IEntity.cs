using com.baensi.sdon.protocol.entities;

namespace com.baensi.sdon.db.entity
{

    /// <summary>
    /// Интерфейс сущности БД
    /// </summary>
    public interface IEntity
    {

        /// <summary>
        /// Устанавливает или возвращает идентификатор текущей сущности
        /// </summary>
        int Id { get; set; }

    }

}
