using Newtonsoft.Json;

namespace com.baensi.sdon.protocol.transport
{

    public class JsonTransportService : TransportServiceBase
    {

        public override string Data<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public override T Get<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

    }

}
