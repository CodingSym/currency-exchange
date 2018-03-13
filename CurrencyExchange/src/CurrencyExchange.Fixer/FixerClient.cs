using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Fixer
{
    public class FixerClient : IFixerClient
    {
        const string URL_PLACEHOLDER_TIMESERIES = "http://data.fixer.io/api/timeseries?access_key={0}&base={1}&symbols={2}&start_date={3}&end_date={4}";
        const string URL_PLACEHOLDER = "http://data.fixer.io/api/latest?access_key={0}&base={1}";
        const string URL_PLACEHOLDER_SYMBOLS = "http://data.fixer.io/api/latest?access_key={0}&base={1}&symbols={2}";
        readonly HttpClient Client;
        string AccessKey;

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
        public async Task<string> GetExchangeRatesFromLast7Days(string baseSymbol, string targetSymbol, DateTime startDate, DateTime endDate)
        {
            var uri = string.Format(URL_PLACEHOLDER_TIMESERIES, AccessKey, baseSymbol, targetSymbol, startDate, endDate);
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
            var uri = string.Format(URL_PLACEHOLDER, AccessKey, baseSymbol);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent("", Encoding.UTF8, "application/json");

            return await SendAsync(requestMessage);
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
