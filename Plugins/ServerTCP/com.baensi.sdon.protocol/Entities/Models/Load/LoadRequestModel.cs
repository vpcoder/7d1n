using System;

namespace com.baensi.sdon.protocol.entities
{

    public class LoadRequestModel : TransportEntity
    {

        public int UserId { get; set; }

        public string GUID { get; set; }

    }

}
