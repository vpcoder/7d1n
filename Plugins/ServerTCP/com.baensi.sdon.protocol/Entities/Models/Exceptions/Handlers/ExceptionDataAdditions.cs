using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.protocol.exceptions;
using System;

namespace com.baensi.sdon.protocol
{

    public static class ExceptionDataAdditions
    {

        public static RemoteException ToException(this ExceptionData data)
        {
            return new RemoteException(data);
        }

        public static ExceptionData ToData(this Exception exception)
        {
            return new ExceptionData()
            {
                Message    = exception.Message,
                StackTrace = exception.StackTrace,
                HelpLink   = exception.HelpLink,
                Source     = exception.Source
            };
        }

    }

}
