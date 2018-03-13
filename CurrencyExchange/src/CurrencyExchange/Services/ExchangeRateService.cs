using CurrencyExchange.Contracts;
using CurrencyExchange.Fixer;
using CurrencyExchange.Helpers;
using CurrencyExchange.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        readonly IConfiguration ConfigurationRoot;
        readonly IFixerClient FixerClient;
        readonly IMathService MathService;
        readonly IResponseParser ResponseParser;
        readonly DateTime utcNow = DateTime.UtcNow;

        public ExchangeRateService(
            IConfiguration configurationRoot, 
            IFixerClient fixerClient, 
            IMathService mathService,
            IResponseParser responseParser)
        {
            ConfigurationRoot = configurationRoot;
            FixerClient = fixerClient;
            MathService = mathService;
            ResponseParser = responseParser;

            FixerClient.AssignApiKey(GetAccessKey());
        }

        public async Task<string> AverageExchangeRateFor7Days(string baseSymbol, string targetSymbol)
        {
            // Validate through IConfigurationService

            var response = await FixerClient.GetExchangeRatesFromLast7Days(baseSymbol, targetSymbol, utcNow.AddDays(-7), utcNow);
            var parsedResponse = ResponseParser.ParseHistoricalRatesResponse(response);

            return MathService.GetAverateRate(parsedResponse.rates).ToString();
        }

        public async Task<string> CurrentExchangeRate(string baseSymbol, string targetSymbol)
        {
            // Validate through IConfigurationService

            var response = await FixerClient.GetCurrentRatesForPair(baseSymbol, targetSymbol);
            var parsedResponse = ResponseParser.ParseExchangeRateResponse(response);

            // Return the actual rate value
            return parsedResponse.rates.First().Value;
        }

        public async Task<string> MaximumExchangeRateDuring7Days(string baseSymbol, string targetSymbol)
        {
            // Validate through IConfigurationService

            var response = await FixerClient.GetExchangeRatesFromLast7Days(baseSymbol, targetSymbol, utcNow.AddDays(-7), utcNow);
            var parsedResponse = ResponseParser.ParseHistoricalRatesResponse(response);

            return MathService.GetMaximumRate(parsedResponse.rates).ToString();
        }

        public async Task<string> MinimumExchangeRateDuring7Days(string baseSymbol, string targetSymbol)
        {
            // Validate through IConfigurationService

            var response = await FixerClient.GetExchangeRatesFromLast7Days(baseSymbol, targetSymbol, utcNow.AddDays(-7), utcNow);
            var parsedResponse = ResponseParser.ParseHistoricalRatesResponse(response);

            return MathService.GetMinimumRate(parsedResponse.rates).ToString();
        }
        public async Task<Dictionary<string, string>> GetAllRates(string baseSymbol)
        {
            // Validate through IConfigurationService

            var response = await FixerClient.GetAllRates(baseSymbol);
            var parsedResponse = ResponseParser.ParseAllRatesResponse(response);

            return parsedResponse.rates;
        }

        string GetAccessKey()
        {
            var fixerSettings = ConfigurationRoot.GetSection("FixerSettings").Get<FixerSettings>();
            return fixerSettings.AccessKey;
        }
    }
}
