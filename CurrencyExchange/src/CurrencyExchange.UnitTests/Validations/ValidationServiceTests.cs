using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace CurrencyExchange.UnitTests.Validations
{
    [Trait(COLLECTION_NAME, COLLECTION_DESCRIPTION)]
    [Collection(DEFINITION_NAME)]
    public class ValidationServiceTests
    {
        const string COLLECTION_NAME = "Validation service tests";
        const string COLLECTION_DESCRIPTION = "Testing proper validation of input models";
        const string DEFINITION_NAME = "unit-tests";

        readonly TestServer Server;
        readonly HttpClient Client;
        readonly IServiceProvider Services;

        public ValidationServiceTests()
        {

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();


            Server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>());
            Services = Server.Host.Services;

            Client = Server.CreateClient();
        }


        [Fact(DisplayName = "Valid / Base Symbol")]
        public void Valid_BaseSymbol()
        {
            // Arrange
            var validator = Services.GetRequiredService<IValidationService>();

            // Act
            var isValidated = validator.IsProperBaseSymbol("EUR");

            // Assert
            Assert.True(isValidated);
        }

        [Fact(DisplayName = "Invalid / Base Symbol")]
        public void Invalid_BaseSymbol()
        {
            // Arrange
            var validator = Services.GetRequiredService<IValidationService>();

            // Act
            var isValidated = validator.IsProperBaseSymbol("USD");

            // Assert
            Assert.False(isValidated);
        }

        [Fact(DisplayName = "Configuration Pair / Does not exist")]
        public void Valid_Configuration_Pair()
        {
            // Arrange
            var validator = Services.GetRequiredService<IValidationService>();
            var configurationService = Services.GetRequiredService<IConfigurationService>();

            // Act
            configurationService.AddCurrency(new SymbolConfigurationDTO("EUR", "PLN"));
            var exists = validator.DoesConfigurationPairExist("EUR", "PLN");

            // Assert
            Assert.True(exists);
        }

        [Fact(DisplayName = "Configuration Pair / Exists")]
        public void Invalid_Configuration_Pair()
        {
            // Arrange
            var validator = Services.GetRequiredService<IValidationService>();

            // Act
            var isValidated = validator.DoesConfigurationPairExist("EUR", "PLN");

            // Assert
            Assert.False(isValidated);
        }


    }
}
