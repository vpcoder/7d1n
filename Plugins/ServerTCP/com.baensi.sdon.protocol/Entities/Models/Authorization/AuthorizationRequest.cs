using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.baensi.sdon.protocol.entities
{

    /// <summary>
    /// Модель запроса авторизации
    /// </summary>
    public class AuthorizationRequest : TransportEntity
    {

        /// <summary>
        /// Идентификатор устройства
        /// </summary>
        public string GUID { get; set; }

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
