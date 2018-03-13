using System.Collections.Generic;

namespace CurrencyExchange.Contracts
{
    public interface IMathService
    {
        double GetAverateRate(Dictionary<string, string> rates);
        double GetMinimumRate(Dictionary<string, string> rates);
        double GetMaximumRate(Dictionary<string, string> rates);
    }
}
