using CurrencyExchange.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchange.Services
{
    public class MathService : IMathService
    {
        public decimal GetAverateRate(List<CurrencyRate> rates) => rates.Average(rate => rate.Rate);

        public decimal GetMaximumRate(List<CurrencyRate> rates) => rates.Max(rate => rate.Rate);

        public decimal GetMinimumRate(List<CurrencyRate> rates) => rates.Min(rate => rate.Rate);
    }
}
