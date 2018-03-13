namespace CurrencyExchange.ErrorModels
{
    public class ExchangeRatesErrorModel
    {
        public ExchangeRatesErrorModel(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Error message
        /// </summary>
        public string Message { get; private set; }
    }
}
