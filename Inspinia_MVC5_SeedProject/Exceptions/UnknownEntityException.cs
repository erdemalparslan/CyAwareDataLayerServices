using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Exceptions
{
    public class UnknownEntityException : Exception
    {
        public UnknownEntityException()
        {
        }

        public UnknownEntityException(string message)
        : base(message)
        {
        }

        public UnknownEntityException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}