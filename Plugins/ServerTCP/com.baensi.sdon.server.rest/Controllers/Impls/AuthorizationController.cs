using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.protocol.exceptions;
using com.baensi.sdon.server.cache;
using System.Web.Http;

namespace com.baensi.sdon.server.rest.controllers
{

    public class AuthorizationController : ControllerBase
    {

        /// <summary>
        /// Метод выполняет авторизацию
        /// </summary>
        /// <param name="request">Запрос авторизации</param>
        /// <returns>Возвращает ответ в виде сущности пользователя</returns>
        [HttpPost]
        public AuthorizationResponse TryAuthorization([FromBody] AuthorizationRequest request)
        {
            return TryT(() =>
            {
                var user = TryFindUser(request);

                var response = new AuthorizationResponse();
                response.User = user.User.Transport;

                return response;
            });
        }

        #region Handlers

        /// <summary>
        /// Ищет пользователя в кэше по запросу
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <returns>Возвращает закешированную модель пользователя</returns>
        private UserCacheModel TryFindUser(AuthorizationRequest request)
        {
            var user = CacheFactory.Instance.Get<UserCacheDictionary>().GetFirst((item) =>
            {
                foreach (var device in item.Devices)
                {
                    if (device.Guid == request.GUID)
                        return true;
                }

                return (item.User.Mail == request.EMail) && (item.User.Pass == request.Password);
            });

            if (user == null)
                throw new UserNotFoundException();

            return user;
        }

        #endregion

    }

}
