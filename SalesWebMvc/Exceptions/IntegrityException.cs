using System;

namespace SalesWebMvc.Exceptions
{
    public class IntegrityException : AbstractException
    {
        public new int HttpStatus { get; } = 400;

        public IntegrityException(string message) : base(message)
        {

        }
    }
}
