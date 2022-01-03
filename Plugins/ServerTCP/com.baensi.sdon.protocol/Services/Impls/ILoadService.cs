using System;
using System.Collections.Generic;

namespace com.baensi.sdon.protocol.services
{

    public interface ILoadService
    {

        /// <summary>
        /// Возвращает полный набор данных о текущем пользователе
        /// </summary>
        /// <param name="loadRequestModel">Модель LoadRequestModel</param>
        /// <returns>Модель LoadResponseModel</returns>
        string LoadUserData(string loadRequestModel);

    }

}
