using com.baensi.sdon.db.dao;
using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.protocol.exceptions;
using com.baensi.sdon.server.cache;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;

namespace com.baensi.sdon.server.rest.controllers
{

    public class LoadController : ControllerBase
    {

        /// <summary>
        /// Метод выполняет регистрацию
        /// </summary>
        /// <param name="request">Запрос регистрации</param>
        /// <returns>Возвращает ответ в виде сущности ответа регистрации</returns>
        [HttpPost]
        public LoadResponseModel LoadUserData([FromBody] LoadRequestModel request)
        {
            return TryT(() =>
            {

                var user = CacheFactory.Instance.Get<UserCacheDictionary>().Get(request.UserId);
                var bag = CacheFactory.Instance.Get<UserBagCacheDictionary>().Get(request.UserId);

                return new LoadResponseModel()
                {
                    UserState = user.State.Transport,
                    UserExp = user.Exp.Transport,
                    UserSkills = user.Skills.ToTransport(),
                    UserEquips = bag.Equips.ToTransport(),
                    UserBags = bag.Bags.Select(o => o.Bag.Transport),
                    UserItems = bag.Bags.SelectMany(o => o.Items).Select(o => o.Transport)
                };

            });
        }

    }

}
