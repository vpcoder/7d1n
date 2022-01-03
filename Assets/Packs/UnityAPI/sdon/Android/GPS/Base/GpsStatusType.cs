using UnityEngine;

namespace Engine.IO
{

    public enum GpsStatusType : byte
    {
        Stopped      = 0,
        Initializing = 1,
        Running      = 2,
        Failed       = 3
    }

    public static class GpsStatusTypeAdditions
    {

        public static GpsStatusType ToGpsStatusType(this LocationServiceStatus status)
        {
            return (GpsStatusType)((int)status);
        }

        public static LocationServiceStatus ToLocationServiceStatus(this GpsStatusType status)
        {
            return (LocationServiceStatus)((byte)status);
        }

    }

}
