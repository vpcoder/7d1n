using com.baensi.sdon.db.dao;
using com.baensi.sdon.logic;
using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.protocol.exceptions;
using com.baensi.sdon.server.cache;
using System;
using System.Web.Http;

namespace com.baensi.sdon.server.rest.controllers
{

    public class RegistrationController : ControllerBase
    {

        /// <summary>
        /// Метод выполняет регистрацию
        /// </summary>
        /// <param name="request">Запрос регистрации</param>
        /// <returns>Возвращает ответ в виде сущности ответа регистрации</returns>
        [HttpPost]
        public RegistrationResponse TryRegistration([FromBody] RegistrationRequest request)
        {
            return TryT(() =>
            {

                if (!IsUnique(request))
                    throw new UserAlreadyExistsException();

                return new RegistrationResponse()
                {
                    User = RegisterUser(request)
                };

            });
        }

        #region Registration

        private bool IsUnique(RegistrationRequest request)
        {
            return DaoFactory.Instance.User.Cache.GetFirst(user =>
            {
                return user.Mail == request.EMail;
            }) == null;
        }

        private User RegisterUser(RegistrationRequest request)
        {
            var user = new db.entity.User();
            user.Mail = request.EMail;
            user.Nick = request.Nick;
            user.Pass = request.Password;
            user.LastUpdate = DateTime.UtcNow.ToTimestamp();
            DaoFactory.Instance.User.Insert(user); // Добавляем пользователя в БД

            var device = new db.entity.UserDevice();
            device.UserId = user.Id;
            device.Guid = request.GUID;
            DaoFactory.Instance.UserDevice.Insert(device); // Добавляем устройство и связываем его с пользователем

            CreateDefaultUserStates(user);

            return user.Transport;
        }

        private void CreateDefaultUserStates(db.entity.User user)
        {
            DaoFactory.Instance.UserExp.Insert(new db.entity.UserExp() // Инициализируем поле опыта
            {
                UserId = user.Id,

                AttackExp = 0,
                CraftExp = 0,
                LootExp = 0,
                ScrapExp = 0,
                WalkExp = 0,

                AttackLvl = 1,
                CraftLvl = 1,
                LootLvl = 1,
                ScrapLvl = 1,
                WalkLvl = 1
            });

            DaoFactory.Instance.UserState.Insert(new db.entity.UserState()
            {
                UserId = user.Id,

                Points = 0,

                Agility = 1,
                Intellect = 1,
                Strength = 1,
                Stamina = 1,

                Health = 100,
                Hunger = 100,
                Infection = 0,
                Defance = 0
            });

            DaoFactory.Instance.UserBag.Insert(new db.entity.UserBag()
            {
                UserId = user.Id,

                Type = (int)UserBagType.UserInventory,
            });
        }

        #endregion

    }

}
