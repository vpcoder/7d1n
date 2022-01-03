using com.baensi.sdon.clientapi.proxies;

namespace com.baensi.sdon.clientapi
{

	public class WebApiFactory
	{

        #region Singleton

        private static WebApiFactory _instance;

		public static WebApiFactory Instance
		{
			get
			{
                if(_instance == null)
                {
                    _instance = new WebApiFactory();
                }
				return _instance;
			}
		}

		#endregion

		#region Shared Api Controllers

		public Load Load { get; private set; }
		public Registration Registration { get; private set; }
		public Authorization Authorization { get; private set; }

		#endregion

		public void InitConnect(string host)
		{
			this.Load = new Load(host);
			this.Registration = new Registration(host);
			this.Authorization = new Authorization(host);
		}
	}

}
