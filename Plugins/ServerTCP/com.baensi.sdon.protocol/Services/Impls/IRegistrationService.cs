using System;

namespace com.baensi.sdon.protocol.services
{

    /// <summary>
    /// Интерфейс сервиса регистрации
    /// </summary>
    public interface IRegistrationService : ITcpService
    {

        /// <summary>
        /// Выполняет запрос регистрации
        /// </summary>
        /// <param name="requestModel">Модель RegistrationRequest</param>
        /// <returns>Модель RegistrationResponse</returns>
        string TryRegistration(string data);

    }

}
