using System;

namespace com.baensi.sdon.protocol.entities
{

    public class RegistrationRequest : TransportEntity
    {

        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// Адрес почты
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string Password { get; set; }

    }

}
