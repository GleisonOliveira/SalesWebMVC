using System;

namespace SalesWebMvc.Exceptions
{
    public class DBConcurrencyUpdateException : AbstractException
    {
        public new int HttpStatus { get; } = 404;
        public DBConcurrencyUpdateException(string message) : base(message)
        {

        }
    }
}
