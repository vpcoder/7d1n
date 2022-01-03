using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.baensi.sdon.protocol.entities
{

    /// <summary>
    /// Ответ на запрос авторизации
    /// </summary>
    public class AuthorizationResponse : TransportEntity
    {

        public User User { get; set; }
        
    }

}
