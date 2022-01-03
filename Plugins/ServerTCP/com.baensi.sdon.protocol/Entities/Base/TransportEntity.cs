using System;

namespace com.baensi.sdon.protocol.entities
{
    
    /// <summary>
    /// Базовый класс транспортной сущности
    /// </summary>
    public abstract class TransportEntity : ITransportEntity
    {

        public ExceptionData ex { get; set; } = null;

    }

}
