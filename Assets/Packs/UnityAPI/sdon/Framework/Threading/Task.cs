using System;
using System.Threading;

namespace UnityEngine {

	public delegate void TaskThread();
	public delegate void TaskException(Exception exception);

	public static class Task {

		public static void Sleep(int millisecondsTimeout) {
			Thread.Sleep(millisecondsTimeout);
		}

		public static void Do(TaskThread thread, TaskException exception = null) {
			try {
				new Thread(() => {
					try {
						thread();
					} catch (Exception ex) {
						if (exception != null) {
							exception(ex);
						}
					}
				}).Start();
			} catch (Exception ex) {
				if (exception != null) {
					exception(ex);
				}
			}
		}

		public static void AddPool(TaskThread thread, TaskException exception = null) {
			try {
				ThreadPool.QueueUserWorkItem((obj) => {
					try {
						thread();
					} catch (Exception ex) {
						if (exception != null) {
							exception(ex);
						}
					}
				});
			} catch (Exception ex) {
				if (exception != null) {
					exception(ex);
				}
			}
		}

	}

}
