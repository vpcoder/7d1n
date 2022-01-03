using com.baensi.sdon.protocol.entities;
using System;

namespace com.baensi.sdon.protocol.exceptions
{

    public class RemoteException : Exception
    {

        private ExceptionData exception;

        public RemoteException(ExceptionData exception) : base(exception.Message)
        {
            this.exception = exception;
        }

        public override string StackTrace
        {
            get
            {
                return this.exception.StackTrace;
            }
        }

        public override string Source
        {
            get
            {
                return this.exception.Source;
            }
        }

        public override string HelpLink
        {
            get
            {
                return this.exception.HelpLink;
            }
        }

    }

}
