using com.baensi.sdon.protocol.entities;
using System;
using System.Collections.Generic;

namespace com.baensi.sdon.db.entity
{

    /// <summary>
    /// Базовый класс сущности БД
    /// </summary>
    public abstract class DbEntity : IEntity
    {
        
        /// <summary>
        /// Устанавливает или возвращает идентификатор текущей сущности
        /// </summary>
        public int Id { get; set; }

    }

}
