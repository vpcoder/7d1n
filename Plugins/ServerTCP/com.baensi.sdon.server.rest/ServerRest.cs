using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using com.baensi.sdon.server.rest.formatters;
using NLog;

namespace com.baensi.sdon.server.rest
{

    public class ServerRest
    {

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public string Host { get; private set; }

        private HttpSelfHostServer server;

        public bool Status { get; private set; } = false;
        
        /// <summary>
        /// Starts REST API server.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="errorCallback">The callback that raised when thrown exception.</param>
        public async Task StartAsync(string url, Action<Exception> errorCallback = null)
        {
            try
            {
                this.Host = url;
                HttpSelfHostConfiguration config = CreateConfiguration(url);
                server = new HttpSelfHostServer(config);

                logger.Debug($"rest server open connection...");
                await server.OpenAsync();
                
                logger.Debug($"rest server started on address: '{url}'");
                Status = true;
            }
            catch (Exception ex)
            {
                Status = false;
                logger.Error(ex);
                errorCallback?.Invoke(ex);
            }
        }

        private HttpSelfHostConfiguration CreateConfiguration(string url)
        {
            logger.Debug($"rest server create configuration...");

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(url);

            config.Formatters.Clear();
            config.Formatters.Add(new JsonRestFormatter());

            config.TransferMode = TransferMode.Streamed;
            config.MaxReceivedMessageSize = 1 * 1024 * 1024; // 4 MiB

            logger.Debug($"rest server make route...");
            config.Routes.MapHttpRoute("ServerAPI", "API/{controller}/{action}");
			config.UserNamePasswordValidator = new Test();

            return config;
        }

    }

	class Test : System.IdentityModel.Selectors.UserNamePasswordValidator
	{
		public override void Validate(string userName, string password)
		{
			
		}
	}

}
