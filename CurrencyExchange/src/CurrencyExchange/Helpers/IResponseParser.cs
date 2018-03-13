using CurrencyExchange.Responses;

namespace CurrencyExchange.Helpers
{
    public interface IResponseParser
    {
        FixerSuccessHistoricalExchangeRateResponse ParseHistoricalRatesResponse(string responseString);
        FixerSuccessExchangeRateResponse ParseExchangeRateResponse(string responseString);
        FixerSuccessAllRatesResponse ParseAllRatesResponse(string responseString);
    }
}