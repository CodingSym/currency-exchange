using System;
using System.Collections.Generic;

namespace CurrencyExchange.Responses
{
    public class FixerSuccessHistoricalExchangeRateResponse
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public bool historical { get; set; }
        public string @base { get; set; }
        public DateTime date { get; set; }
        public Dictionary<string, string> rates { get; set; }
    }
}
