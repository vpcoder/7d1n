using System;

namespace com.baensi.sdon.clientapi.handlers.threading
{

	public static class AsyncService
	{

		private static void DoThread(Action action)
		{
			new System.Threading.Thread(()=>
			{
				action();
			}).Start();
		}

		public static void DoPromise<T>(Func<T> func,
										Action<T> callback,
										Action<Exception> exceptionCallback = null)
		{
			try
			{
				DoThread(() =>
				{
					try
					{
						var value = func();
						callback(value);
					}
					catch (Exception ex)
					{
						try
						{

							exceptionCallback?.Invoke(ex);
						}
						catch { }
					}
				});
			}
			catch (Exception ex)
			{
				try
				{
					exceptionCallback?.Invoke(ex);
				}
				catch { }
			}
		}

	}

}
