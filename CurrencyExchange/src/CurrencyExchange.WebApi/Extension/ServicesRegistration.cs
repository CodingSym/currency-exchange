using CurrencyExchange.Contracts;
using CurrencyExchange.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.WebApi.Extension
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IValuesService, ValueService>();
        }
    }
}
