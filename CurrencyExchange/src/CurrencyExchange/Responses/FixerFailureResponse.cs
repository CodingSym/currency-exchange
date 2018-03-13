namespace CurrencyExchange.Responses
{
    public class FixerFailureResponse
    {
        public bool success { get; set; }
        public Error error { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string type { get; set; }
    }
}
