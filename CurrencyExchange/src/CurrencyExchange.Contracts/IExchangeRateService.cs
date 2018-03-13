using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Contracts
{
    public interface IExchangeRateService
    {
        Task<string> CurrentExchangeRate(string baseSymbol, string targetSymbol);
        Task<string> AverageExchangeRateFor7Days(string baseSymbol, string targetSymbol);
        Task<string> MinimumExchangeRateDuring7Days(string baseSymbol, string targetSymbol);
        Task<string> MaximumExchangeRateDuring7Days(string baseSymbol, string targetSymbol);
        Task<Dictionary<string, string>> GetAllRates (string baseSymbol);
    }
}
