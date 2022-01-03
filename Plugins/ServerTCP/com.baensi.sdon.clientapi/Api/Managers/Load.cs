using System;
using System.Linq;
using System.Collections.Generic;
using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.clientapi.handlers.threading;

namespace com.baensi.sdon.clientapi.proxies
{

	public class Load : RestClient, ILoad
	{

		public override string ControllerName { get; } = "Load";

		#region Ctor

		public Load(string host) : base (host)
		{ }

		#endregion

		#region API

		/// <summary>
        /// Autogen Удалённый метод LoadUserData контроллера LoadController
        /// </summary>
		public LoadResponseModel LoadUserData(LoadRequestModel request)
		{
			return Proxy.Invoke<LoadResponseModel>(ToURL("LoadUserData"), request);
		}
		
		/// <summary>
        /// Autogen Удалённый метод LoadUserData контроллера LoadController, работающий в параллельном потоке
        /// </summary>
		public void LoadUserDataPromise(LoadRequestModel request, Action<LoadResponseModel> callback, Action<Exception> exceptionCallback = null)
		{
			AsyncService.DoPromise(()=>
			{
				return LoadUserData(request);
			}, callback, exceptionCallback);
		}
		
		#endregion

	}

}
