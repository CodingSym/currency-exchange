using System.Collections.Generic;

namespace CurrencyExchange.Contracts
{
    public interface IMathService
    {
        decimal GetAverateRate(List<CurrencyRate> rates);
        decimal GetMinimumRate(List<CurrencyRate> rates);
        decimal GetMaximumRate(List<CurrencyRate> rates);
    }
}
