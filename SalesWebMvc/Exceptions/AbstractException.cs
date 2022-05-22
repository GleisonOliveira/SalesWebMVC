using System;

namespace SalesWebMvc.Exceptions
{
    public abstract class AbstractException : ApplicationException
    {
        public int HttpStatus { get; }

        public AbstractException(string message) : base(message)
        {

        }
    }
}
