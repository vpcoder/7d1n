using System.Collections.Generic;
using System;

namespace UnityEngine
{

    public class UnityThreadData
    {
        public static readonly Queue<Action> _executionQueue = new Queue<Action>();
        public static long count = 0;

        public static void Do(Action action)
        {
            lock (_executionQueue)
            {
                count++;
                _executionQueue.Enqueue(() =>
                {
                    action();
                });
            }
        }

    }

    public class UnityThread : MonoBehaviour
    {

        public void Update()
        {
            if (UnityThreadData.count == 0)
                return;

            lock (UnityThreadData._executionQueue)
            {
                while (UnityThreadData._executionQueue.Count > 0)
                {
                    UnityThreadData._executionQueue.Dequeue().Invoke();
                }
                UnityThreadData.count = 0;
            }
        }

    }

}
