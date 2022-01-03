using System;

namespace com.baensi.sdon.protocol.exceptions
{

    public class UserNotFoundException : Exception
    {

        public UserNotFoundException() : base("user with this email or device not found!")
        { }

    }

}
