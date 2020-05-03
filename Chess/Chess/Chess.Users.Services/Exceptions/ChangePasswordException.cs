using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Users.Services.Exceptions
{
    public class ChangePasswordException : Exception
    {
        public ChangePasswordException() { }
        public ChangePasswordException(string message) : base(message) { }
        public ChangePasswordException(string message, Exception inner) : base(message, inner) { }
        protected ChangePasswordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
