using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOP_WCF
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
        {
        }
        public LoginFailedException(string message)
        : base(message)
        {
        }
        public LoginFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}