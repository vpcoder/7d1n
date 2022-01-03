using System;
using System.Linq;
using System.Collections.Generic;
using com.baensi.sdon.protocol.entities;

namespace com.baensi.sdon.clientapi
{

	/// <summary>
    /// Autogen API
    /// </summary>
	public interface IAuthorization : IRemoteApi
	{

		#region Methods

		AuthorizationResponse TryAuthorization(AuthorizationRequest request);

		#endregion
		
		#region Promise Methods

		void TryAuthorizationPromise(AuthorizationRequest request, Action<AuthorizationResponse> callback, Action<Exception> exceptionCallback = null);
		
		#endregion

	}

}
