﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOP_WCF
{
    public class NoAvailableTodoException : Exception
    {
        public NoAvailableTodoException()
        {
        }
        public NoAvailableTodoException(string message)
        : base(message)
        {
        }
        public NoAvailableTodoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}