using com.baensi.sdon.protocol.entities;
using System;

namespace com.baensi.sdon.protocol.transport
{

    /// <summary>
    /// Прослойка отвечающая за работу с HTTP
    /// </summary>
    public class TransportWebClient : HttpBase
    {

        #region Ctor

        public TransportWebClient(string host) : base(host)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Вызывает REST-метод на удалённом сервере
        /// </summary>
        /// <typeparam name="T">Модель, возвращаемая удалённым сервером</typeparam>
        /// <param name="url">Адрес удалённого API метода</param>
        /// <param name="arg">Аргумент, передаваемый удалённому серверу</param>
        /// <returns>Возвращает модель указанного типа</returns>
        /// <exception cref="exceptions.RemoteException"></exception>
        /// <exception cref="Exception"></exception>
        public T Invoke<T>(string url, TransportEntity arg) where T : TransportEntity
        {
            var json = SyncPost(url, arg);
            var model = json.ToTransport<T>();

            if (model.ex != null)
                throw model.ex.ToException();

            return model;
        }

        /// <summary>
        /// Вызывает REST-метод на удалённом сервере
        /// </summary>
        /// <param name="url">Адрес удалённого API метода</param>
        /// <param name="arg">Аргумент, передаваемый удалённому серверу</param>
        /// <exception cref="exceptions.RemoteException"></exception>
        /// <exception cref="Exception"></exception>
        public void Invoke(string url, TransportEntity arg)
        {
            var json = SyncPost(url, arg);
            var model = json.ToTransport<TransportEntity>();

            if (model.ex != null)
                throw model.ex.ToException();
        }

        #endregion

    }
}
