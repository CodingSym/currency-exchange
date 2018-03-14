using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Fixer
{
    public class FixerClient : IFixerClient
    {
        const string URL_PLACEHOLDER_DATE = "http://data.fixer.io/api/{3}?access_key={0}&base={1}&symbols={2}";
        const string URL_PLACEHOLDER = "http://data.fixer.io/api/latest?access_key={0}&base={1}";
        const string URL_PLACEHOLDER_SYMBOLS = "http://data.fixer.io/api/latest?access_key={0}&base={1}&symbols={2}";
        readonly HttpClient Client;
        string AccessKey;
        static string GetAllRatesResponse = "";

        public FixerClient(HttpClient client)
        {
            Client = client;
        }

        public void AssignApiKey(string accessKey) => AccessKey = accessKey;
        /// <summary>
        /// Gets exchange rate for specified currency pair
        /// </summary>
        /// <param name="baseSymbol">base currency symbol</param>
        /// <param name="targetSymbol">target currency symbol</param>
        /// <returns></returns>
        public async Task<string> GetCurrentRatesForPair(string baseSymbol, string targetSymbol)
        {
            var uri = string.Format(URL_PLACEHOLDER_SYMBOLS,  AccessKey, baseSymbol, targetSymbol);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
            return await SendAsync(requestMessage);
        }
        /// <summary>
        /// Gets exchange rates for specified currency pair for the past 7 days
        /// </summary>
        /// <param name="baseSymbol">base currency symbol</param>
        /// <param name="targetSymbol">target currency symbol</param>
        /// <returns></returns>
        public async Task<string> GetExchangeRatesForSpecifiedDate(string baseSymbol, string targetSymbol, DateTime date)
        {
            // Cannot use timeseries API to get data for the past 7 days at once 
            // Therefore I have to ask for a specific day 7 times

            var dateString = date.ToString("yyyy-MM-dd");
            var uri = string.Format(URL_PLACEHOLDER_DATE, AccessKey, baseSymbol, targetSymbol, dateString);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
            return await SendAsync(requestMessage);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseSymbol">base currency symbol</param>
        /// <returns></returns>
        public async Task<string> GetAllRates(string baseSymbol)
        {
            if (baseSymbol.ToLower() == "eur" && !string.IsNullOrWhiteSpace(GetAllRatesResponse))
                return GetAllRatesResponse;

            var uri = string.Format(URL_PLACEHOLDER, AccessKey, baseSymbol);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await SendAsync(requestMessage);

            GetAllRatesResponse = response;
            return GetAllRatesResponse;
        }

        async Task<string> SendAsync(HttpRequestMessage requestMessage)
        {
            var response = await Client.SendAsync(requestMessage);

            using (HttpContent content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }
    }
}
