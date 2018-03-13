using CurrencyExchange.Contracts;
using CurrencyExchange.Settings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CurrencyExchange.Services
{
    public class ValueService : IValuesService
    {
        readonly IConfiguration configurationRoot;
        public ValueService(IConfiguration configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }
        public string GetConfig()
        {
            //return "true";
            var fixerSettings = configurationRoot.GetSection("FixerSettings").Get<FixerSettings>();
            return JsonConvert.SerializeObject(fixerSettings);
        }
    }
}
