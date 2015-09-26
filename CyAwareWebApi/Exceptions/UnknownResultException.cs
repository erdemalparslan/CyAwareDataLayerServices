using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Exceptions
{
    public class UnknownResultException : Exception
    {
        public UnknownResultException()
        {
        }

        public UnknownResultException(string message)
        : base(message)
        {
        }

        public UnknownResultException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}