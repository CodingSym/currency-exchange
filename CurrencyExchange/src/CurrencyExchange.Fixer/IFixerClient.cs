using System;
using System.Threading.Tasks;

namespace CurrencyExchange.Fixer
{
    public interface IFixerClient
    {
        void AssignApiKey(string accessKey);
        Task<string> GetAllRates(string baseSymbol);
        Task<string> GetCurrentRatesForPair(string baseSymbol, string targetSymbol);
        Task<string> GetExchangeRatesForSpecifiedDate(string baseSymbol, string targetSymbol, DateTime date);
    }
}