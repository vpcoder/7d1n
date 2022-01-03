using System;

namespace com.baensi.sdon.tools.reflection
{

    public static class Domain
    {

        public static void ForEachTypes(Action<Type> callback, Action<Exception> exceptionCallback = null)
        {
            try
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        foreach (var type in assembly.GetTypes())
                        {
                            try
                            {
                                callback(type);
                            }
                            catch (Exception ex)
                            {
                                exceptionCallback?.Invoke(ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptionCallback?.Invoke(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionCallback?.Invoke(ex);
            }
        }

    }

}
