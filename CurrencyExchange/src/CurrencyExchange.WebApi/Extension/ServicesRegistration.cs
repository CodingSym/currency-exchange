using CurrencyExchange.Contracts;
using CurrencyExchange.Fixer;
using CurrencyExchange.Helpers;
using CurrencyExchange.Repositories;
using CurrencyExchange.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace CurrencyExchange.WebApi.Extension
{
    public static class ServicesRegistration
    {
        public static void RegisterModuleComponents(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ISymbolConfigurationRepository, SymbolConfigurationRepository>();

            // Validators
            services.AddScoped<IValidationService, ValidationService>();

            // Services
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddScoped<IMathService, MathService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();

            // Helpers
            services.AddScoped<IResponseParser, ResponseParser>();

            // Http client
            services.TryAddScoped<HttpClient>();
            services.AddScoped<IFixerClient, FixerClient>();
        }
    }
}
