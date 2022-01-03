using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.baensi.sdon.protocol.exceptions
{

    public class UserAlreadyExistsException : Exception
    {

        public UserAlreadyExistsException() : base("user with this email already exists!")
        { }

    }

}
