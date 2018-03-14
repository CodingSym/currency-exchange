using CurrencyExchange.Contracts;
using CurrencyExchange.Exceptions;
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
        const int DaysCount7 = 7;

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
            var totalRates = await GetCurrencyRatesFor7Days(baseSymbol, targetSymbol);

            return MathService.GetAverateRate(totalRates).ToString();
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

            var totalRates = await GetCurrencyRatesFor7Days(baseSymbol, targetSymbol);

            return MathService.GetMaximumRate(totalRates).ToString();
        }

        public async Task<string> MinimumExchangeRateDuring7Days(string baseSymbol, string targetSymbol)
        {
            // Validate through IConfigurationService

            var totalRates = await GetCurrencyRatesFor7Days(baseSymbol, targetSymbol);

            return MathService.GetMinimumRate(totalRates).ToString();
        }
        public async Task<Dictionary<string, string>> GetAllRates(string baseSymbol)
        {
            // Validate through IConfigurationService

            var response = await FixerClient.GetAllRates(baseSymbol);
            var parsedResponse = ResponseParser.ParseAllRatesResponse(response);

            return parsedResponse.rates;
        }

        async Task<List<CurrencyRate>> GetCurrencyRatesFor7Days(string baseSymbol, string targetSymbol)
        {
            var totalRates = new List<CurrencyRate>();

            for (int i = 0; i < DaysCount7; i++)
            {
                var response = await FixerClient.GetExchangeRatesForSpecifiedDate(baseSymbol, targetSymbol, utcNow.AddDays(-i));
                var parsedResponse = ResponseParser.ParseHistoricalRatesResponse(response);

                if (!parsedResponse.rates.Any()) throw new FixerException("Fixer API shows null rates");

                var rates = parsedResponse.rates.First();

                totalRates.Add(new CurrencyRate(rates.Key, decimal.Parse(rates.Value)));
            }

            return totalRates;
        }

        string GetAccessKey()
        {
            var fixerSettings = ConfigurationRoot.GetSection("FixerSettings").Get<FixerSettings>();
            return fixerSettings.AccessKey;
        }
    }
}
