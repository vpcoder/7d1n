using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.protocol.transport;
using System.Collections.Generic;

namespace com.baensi.sdon
{

    public static class ITransportEntityAdditions
    {

        public static IEnumerable<string> ToData<T>(this IEnumerable<T> set) where T : class, ITransportEntity
        {
            return TransportFactory.Instance.Service.Data(set);
        }

        public static string ToData(this ITransportEntity entity)
        {
            return TransportFactory.Instance.Service.Data(entity);
        }

        public static T ToTransport<T>(this string data) where T : class, ITransportEntity
        {
            return TransportFactory.Instance.Service.Get<T>(data);
        }

        public static IEnumerable<T> ToTransport<T>(this IEnumerable<string> data) where T : class, ITransportEntity
        {
            return TransportFactory.Instance.Service.Get<T>(data);
        }

    }

}
