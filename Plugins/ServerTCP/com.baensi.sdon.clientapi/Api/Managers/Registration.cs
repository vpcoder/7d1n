using System;
using System.Linq;
using System.Collections.Generic;
using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.clientapi.handlers.threading;

namespace com.baensi.sdon.clientapi.proxies
{

	public class Registration : RestClient, IRegistration
	{

		public override string ControllerName { get; } = "Registration";

		#region Ctor

		public Registration(string host) : base (host)
		{ }

		#endregion

		#region API

		/// <summary>
        /// Autogen Удалённый метод TryRegistration контроллера RegistrationController
        /// </summary>
		public RegistrationResponse TryRegistration(RegistrationRequest request)
		{
			return Proxy.Invoke<RegistrationResponse>(ToURL("TryRegistration"), request);
		}
		
		/// <summary>
        /// Autogen Удалённый метод TryRegistration контроллера RegistrationController, работающий в параллельном потоке
        /// </summary>
		public void TryRegistrationPromise(RegistrationRequest request, Action<RegistrationResponse> callback, Action<Exception> exceptionCallback = null)
		{
			AsyncService.DoPromise(()=>
			{
				return TryRegistration(request);
			}, callback, exceptionCallback);
		}
		
		#endregion

	}

}
