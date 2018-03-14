using System;

namespace CurrencyExchange.Exceptions
{
    public class FixerException : Exception
    {
        public FixerException(string message) : base(message)
        {
        }
    }
}
