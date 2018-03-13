using CurrencyExchange.Exceptions;
using CurrencyExchange.Responses;
using Newtonsoft.Json;

namespace CurrencyExchange.Helpers
{
    public class ResponseParser : IResponseParser
    {
        public FixerSuccessHistoricalExchangeRateResponse ParseHistoricalRatesResponse(string responseString)
        {
            CheckIfErrorMessage(responseString);
            return JsonConvert.DeserializeObject<FixerSuccessHistoricalExchangeRateResponse>(responseString);
        }
        public FixerSuccessExchangeRateResponse ParseExchangeRateResponse(string responseString)
        {
            CheckIfErrorMessage(responseString);
            return JsonConvert.DeserializeObject<FixerSuccessExchangeRateResponse>(responseString);
        }
        public FixerSuccessAllRatesResponse ParseAllRatesResponse(string responseString)
        {
            CheckIfErrorMessage(responseString);
            return JsonConvert.DeserializeObject<FixerSuccessAllRatesResponse>(responseString);
        }

        void CheckIfErrorMessage(string responseString)
        {
            if (responseString.Contains("error"))
            {
                var response = JsonConvert.DeserializeObject<FixerFailureResponse>(responseString);
                throw new ExchangeRateException(response.error.type);
            }
        }

    }
}
