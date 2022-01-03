using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.baensi.sdon.protocol.services
{

    /// <summary>
    /// Интерфейс сервиса авторизации
    /// </summary>
    public interface IAuthorizationService : ITcpService
    {

        /// <summary>
        /// Выполняет запрос авторизации
        /// </summary>
        /// <param name="requestModel">Модель AuthorizationRequest</param>
        /// <returns>Модель AuthorizationResponse</returns>
        string TryAuthorization(string requestModel);

    }

}
