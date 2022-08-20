using System;

namespace Engine.DB
{

    /// <summary>
    /// 
    /// Объект БД имеющий идентификатор
    /// ---
    /// A database object that has an identifier
    /// 
    /// </summary>
    [Serializable]
    public abstract class Dto : IDto
    {

        ///<summary>
        ///     Идентификатор объекта
        ///     Обязан быть уникален в рамках своего типа
        ///     ----
        ///     Object identifier
        ///     Must be unique within its type
        ///</summary>
        [PrimaryKey]
        [AutoIncrement]
        [Unique]
        [Column("id")]
        public long ID { get; set; }

    }

}
