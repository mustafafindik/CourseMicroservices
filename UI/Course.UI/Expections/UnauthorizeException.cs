using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.UI.Expections
{
    public class UnauthorizeException : Exception
    {
        public UnauthorizeException() : base()
        {
        }

        public UnauthorizeException(string message) : base(message)
        {
        }

        public UnauthorizeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
