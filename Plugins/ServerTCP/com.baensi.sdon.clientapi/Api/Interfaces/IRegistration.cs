using System;
using System.Linq;
using System.Collections.Generic;
using com.baensi.sdon.protocol.entities;

namespace com.baensi.sdon.clientapi
{

	/// <summary>
    /// Autogen API
    /// </summary>
	public interface IRegistration : IRemoteApi
	{

		#region Methods

		RegistrationResponse TryRegistration(RegistrationRequest request);

		#endregion
		
		#region Promise Methods

		void TryRegistrationPromise(RegistrationRequest request, Action<RegistrationResponse> callback, Action<Exception> exceptionCallback = null);
		
		#endregion

	}

}
