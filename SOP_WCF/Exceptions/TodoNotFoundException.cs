using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOP_WCF
{
    public class TodoNotFoundException : Exception
    {
        public TodoNotFoundException()
        {
        }
        public TodoNotFoundException(string message)
        : base(message)
        {
        }
        public TodoNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}