using System;

namespace Engine.DB
{

    public interface IDto
    {

        ///<summary>
        /// Идентификатор объекта
        /// Обязан быть уникален в рамках своего типа
        ///</summary>
        long ID { get; set; }

    }

    [Serializable]
    public abstract class Dto : IDto
    {

        [PrimaryKey]
        [AutoIncrement]
        [Unique]
        [Column("id")]
        public long ID { get; set; }

    }

}
