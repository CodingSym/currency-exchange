namespace CurrencyExchange.Contracts
{
    public sealed class CurrencyRate
    {
        public CurrencyRate(string symbol, decimal rate)
        {
            Symbol = symbol;
            Rate = rate;
        }

        public string Symbol { get; private set; }
        public decimal Rate { get; private set; }
    }
}
