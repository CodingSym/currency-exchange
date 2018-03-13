using System;

namespace CurrencyExchange.Exceptions
{
    public class ExchangeRateException : Exception
    {
        public ExchangeRateException(string message) : base(message)
        {
        }
    }
}
