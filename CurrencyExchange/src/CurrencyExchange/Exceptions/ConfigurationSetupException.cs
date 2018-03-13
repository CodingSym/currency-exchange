using System;

namespace CurrencyExchange.Exceptions
{
    public class ConfigurationSetupException : Exception
    {
        public ConfigurationSetupException(string message) : base(message)
        {
        }
    }
}
