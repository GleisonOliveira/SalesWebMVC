using System;

namespace SalesWebMvc.Exceptions
{
    public class NotFoundException : AbstractException
    {
        public new int HttpStatus { get; } = 404;

        public NotFoundException(string message) : base(message)
        {

        }
    }
}
