using CurrencyExchange.Contracts.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyExchange.IntegrationTests.Services
{
    [Trait(COLLECTION_NAME, COLLECTION_DESCRIPTION)]
    [Collection(DEFINITION_NAME)]
    public class ExchangeRateTests
    {
        const string COLLECTION_NAME = "Exchange rate service tests";
        const string COLLECTION_DESCRIPTION = "Testing proper exchange of currencies";
        const string DEFINITION_NAME = "integration-tests";

        readonly TestServer Server;
        readonly HttpClient Client;
        readonly IServiceProvider Services;

        public ExchangeRateTests()
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

        [Fact(DisplayName = "Invalid Symbol Configuration")]
        public async Task Invalid_Symbol_Configuration()
        {
            // Arrange
            var configurationRequestURI = "api/configurations";
            var model = new SymbolsPairConfigurationDTO("eurr", "usd");
            var configurationRequest = new HttpRequestMessage(HttpMethod.Post, configurationRequestURI);
            configurationRequest.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); ;

            // Act
            var response = await Client.SendAsync(configurationRequest);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Get Currency Exchange Without Configuration")]
        public async Task Get_Currency_Exchange_Without_Configuration()
        {
            // Arrange
            var request = "/api/exchange-rates/eur/usd";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Get Currency Exchange After Proper Configuration")]
        public async Task Get_Currency_Exchange_After_Configuration()
        {
            // Arrange
            var configurationRequestURI = "api/configurations";
            var model = new SymbolsPairConfigurationDTO("eur", "usd");
            var configurationRequest = new HttpRequestMessage(HttpMethod.Post, configurationRequestURI);
            configurationRequest.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); ;

            var exchangeRequestURI = "/api/exchange-rates/eur/usd";

            // Act
            await Client.SendAsync(configurationRequest);
            var response = await Client.GetAsync(exchangeRequestURI);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Content);
        }

        [Fact(DisplayName = "Get Currency Exchange Average After Proper Configuration")]
        public async Task Get_Currency_Exchange_Average_After_Configuration()
        {
            // Arrange
            var configurationRequestURI = "api/configurations";
            var model = new SymbolsPairConfigurationDTO("eur", "usd");
            var configurationRequest = new HttpRequestMessage(HttpMethod.Post, configurationRequestURI);
            configurationRequest.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); ;

            var exchangeRequestURI = "/api/exchange-rates/eur/usd/average";

            // Act
            await Client.SendAsync(configurationRequest);
            var response = await Client.GetAsync(exchangeRequestURI);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Content);
        }

        [Fact(DisplayName = "Get Currency Exchange Minimum After Proper Configuration")]
        public async Task Get_Currency_Exchange_Minimum_After_Configuration()
        {
            // Arrange
            var configurationRequestURI = "api/configurations";
            var model = new SymbolsPairConfigurationDTO("eur", "usd");
            var configurationRequest = new HttpRequestMessage(HttpMethod.Post, configurationRequestURI);
            configurationRequest.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); ;

            var exchangeRequestURI = "/api/exchange-rates/eur/usd/minimum";

            // Act
            await Client.SendAsync(configurationRequest);
            var response = await Client.GetAsync(exchangeRequestURI);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Content);
        }

        [Fact(DisplayName = "Get Currency Exchange Maximum After Proper Configuration")]
        public async Task Get_Currency_Exchange_Maximum_After_Configuration()
        {
            // Arrange
            var configurationRequestURI = "api/configurations";
            var model = new SymbolsPairConfigurationDTO("eur", "usd");
            var configurationRequest = new HttpRequestMessage(HttpMethod.Post, configurationRequestURI);
            configurationRequest.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); ;

            var exchangeRequestURI = "/api/exchange-rates/eur/usd/maximum";

            // Act
            await Client.SendAsync(configurationRequest);
            var response = await Client.GetAsync(exchangeRequestURI);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.NotNull(response.Content);
        }


        [Fact(DisplayName = "Invalid Base Symbol")]
        public async Task Invalid_Base_Symbol()
        {
            // Arrange
            var exchangeRateURI = "api/exchange-rates/pln";
            
            // Act
            var response = await Client.GetAsync(exchangeRateURI);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }


        [Fact(DisplayName = "Valid Base Symbol")]
        public async Task Valid_Base_Symbol()
        {
            // Arrange
            var exchangeRateURI = "api/exchange-rates/eur";

            // Act
            var response = await Client.GetAsync(exchangeRateURI);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
