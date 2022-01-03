using System;

namespace com.baensi.sdon.protocol.transport
{

    public class TransportFactory
    {

        #region Singleton

        private static TransportFactory _instance;

        public static TransportFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TransportFactory();
                return _instance;
            }
        }

        #endregion

        #region Properties

        public ITransportService Service { get; }

        #endregion

        #region Ctors

        public TransportFactory()
        {
            Service = new JsonTransportService();
        }

        #endregion


    }

}
