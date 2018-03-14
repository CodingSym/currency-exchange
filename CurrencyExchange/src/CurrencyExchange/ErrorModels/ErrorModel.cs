namespace CurrencyExchange.ErrorModels
{
    public class ErrorModel
    {
        public ErrorModel(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Error message
        /// </summary>
        public string Message { get; private set; }
    }
}
