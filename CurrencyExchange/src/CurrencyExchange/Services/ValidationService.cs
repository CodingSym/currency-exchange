using CurrencyExchange.Contracts;
using CurrencyExchange.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class ValidationService : IValidationService
    {
        readonly ISymbolConfigurationRepository Repository;
        readonly IExchangeRateService ExchangeRateService;

        public ValidationService(ISymbolConfigurationRepository repository, IExchangeRateService exchangeRateService)
        {
            Repository = repository;
            ExchangeRateService = exchangeRateService;
        }

        public bool IsProperBaseSymbol(string baseSymbol)
        {
            return baseSymbol.ToLower() == "eur";
        }

        public bool DoesConfigurationPairExist(string baseSymbol, string targetSymbol)
        {
            return Repository.Get(baseSymbol, targetSymbol) != null;
        }

        public async Task<bool> DoesSuchASymbolExist(string symbol)
        {
            var symbols = await ExchangeRateService.GetAllRates("EUR");

            return symbols.Any(symbolPair => symbolPair.Key.ToLower() == symbol.ToLower());
        }
    }
}
