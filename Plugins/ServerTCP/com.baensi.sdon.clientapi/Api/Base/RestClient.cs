using com.baensi.sdon.protocol.transport;

namespace com.baensi.sdon.clientapi
{

    public abstract class RestClient
    {

        #region Hidden Fields

        protected TransportWebClient Proxy { get; }

        #endregion

        #region Properties

        public string Host { get; set; }

        public abstract string ControllerName { get;}

        #endregion

        public RestClient(string host)
        {
            this.Host = host;
            this.Proxy = new TransportWebClient(host);
        }

        protected string ToURL(string methodName)
        {
            return $"API/{ControllerName}/{methodName}";
        }

    }

}
