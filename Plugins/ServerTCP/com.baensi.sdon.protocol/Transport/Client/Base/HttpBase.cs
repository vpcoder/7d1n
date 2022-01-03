using com.baensi.sdon.protocol.entities;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.baensi.sdon.protocol.transport
{

    /// <summary>
    /// Базовый Http-клиент, позволяющий работать с HTTP запросами
    /// </summary>
    public abstract class HttpBase
    {

        #region Constants

        /// <summary>
        /// Максимальное время ожидания (в миллисекундах) при синхронном выполнении запроса
        /// </summary>
        public static readonly TimeSpan QueryTimeout = TimeSpan.FromMilliseconds(30000);

        #endregion

        #region Properties

        /// <summary>
        /// Адрес удалённого сервера
        /// </summary>
        public string Host { get; }

        #endregion

        #region Ctors

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="host">Адрес</param>
        public HttpBase(string host)
        {
            this.Host = NormalizeUri(host);
        }

        #endregion

        #region Helps Handlers

        /// <summary>
        /// Возвращает полный путь запроса (включая адрес сервера)
        /// </summary>
        /// <param name="uri">Фрагмент запроса</param>
        /// <returns>Возвращает полный путь запроса</returns>
        private string GetURL(string uri)
        {
            return Host + uri;
        }

        /// <summary>
        /// Нормализует формат URI строки к виду "valid/uri/path/"
        /// </summary>
        /// <param name="uri">Исходный фрагмент uri</param>
        /// <returns>Возвращает нормализованную строку URI</returns>
        private string NormalizeUri(string uri)
        {
            var url = new StringBuilder(255);

            if (uri.StartsWith("/"))
                uri = uri.Substring(1, uri.Length - 1);

            url.Append(uri.Replace("\\", "/"));

            if (!uri.EndsWith("/"))
                url.Append("/");

            return url.ToString();
        }

        #endregion

        /// <summary>
        /// Посылает POST-запрос на удалённый сервер в синхронном режиме
        /// </summary>
        /// <param name="requestUri">URI запроса</param>
        /// <param name="requestArgs">Аргумент запроса</param>
        /// <returns>Возвращает тело ответа, полученного от удалённого сервера (ожидается что это будет json)</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="TimeoutException"></exception>
        /// <exception cref="Exception"></exception>
        /// <example>
        /// Пример использования метода
        /// <code><![CDATA[
        /// 
        ///     // Делаем POST (не GET, а именно POST) запрос гуглу, читаем ответ в виде строки
        ///     var html = client.SyncPost("http://google.com");
        ///     
        ///     // Выводим прочитанную строку
        ///     System.Console.WriteLine(html);
        /// 
        /// ]]></code>
        /// </example>
        protected virtual string SyncPost<T>(string requestUri, T args = null) where T : class, ITransportEntity
        {
            try
            {
                if (string.IsNullOrEmpty(requestUri))
                    throw new ArgumentNullException("requestUri");

                var url = GetURL(requestUri);
                var json = args.ToData();

                // Посылаем запрос
                var data = SyncPost(url, json);

                return data;
            }
			catch (WebException ex)
			{
				throw ex;
			}
            catch (Exception ex)
            {
                if (ex.InnerException is TaskCanceledException)
                    throw new TimeoutException($"Server request timeout {nameof(SyncPost)}", ex);

                return null;
            }
        }

        protected string SyncPost(string address, string data)
        {
            using (var client = new WebClient())
            {
                client.Headers.Set("Content-Type", "application/json");
                client.Headers.Set("Authorization", "Basic YW5vbjplbXB0eQ==");

                var bytes = Encoding.ASCII.GetBytes(data);
                var result = client.UploadData(new Uri(address), "POST", bytes);

                return Encoding.UTF8.GetString(result);
            }
        }

    }

}
