using System;
using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.clientapi.handlers.threading;

namespace com.baensi.sdon.clientapi.proxies
{

	public class Authorization : RestClient, IAuthorization
	{

		public override string ControllerName { get; } = "Authorization";

		#region Ctor

		public Authorization(string host) : base (host)
		{ }

		#endregion

		#region API

		/// <summary>
        /// Autogen Удалённый метод TryAuthorization контроллера AuthorizationController
        /// </summary>
		public AuthorizationResponse TryAuthorization(AuthorizationRequest request)
		{
			return Proxy.Invoke<AuthorizationResponse>(ToURL("TryAuthorization"), request);
		}
		
		/// <summary>
        /// Autogen Удалённый метод TryAuthorization контроллера AuthorizationController, работающий в параллельном потоке
        /// </summary>
		public void TryAuthorizationPromise(AuthorizationRequest request, Action<AuthorizationResponse> callback, Action<Exception> exceptionCallback = null)
		{
			AsyncService.DoPromise(()=>
			{
				return TryAuthorization(request);
			}, callback, exceptionCallback);
		}
		
		#endregion

	}

}
