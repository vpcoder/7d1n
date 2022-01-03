using System;
using System.Linq;
using System.Collections.Generic;
using com.baensi.sdon.protocol.entities;

namespace com.baensi.sdon.clientapi
{

	/// <summary>
    /// Autogen API
    /// </summary>
	public interface ILoad : IRemoteApi
	{

		#region Methods

		LoadResponseModel LoadUserData(LoadRequestModel request);

		#endregion
		
		#region Promise Methods

		void LoadUserDataPromise(LoadRequestModel request, Action<LoadResponseModel> callback, Action<Exception> exceptionCallback = null);
		
		#endregion

	}

}
