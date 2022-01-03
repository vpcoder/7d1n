using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.baensi.sdon.protocol.entities
{

    public class ExceptionData : ITransportEntity
    {

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string Source { get; set; }

        public string HelpLink { get; set; }

    }

}
