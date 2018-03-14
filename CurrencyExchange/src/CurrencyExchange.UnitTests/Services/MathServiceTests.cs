using CurrencyExchange.Contracts.DTOs;
using CurrencyExchange.Services;
using System.Collections.Generic;
using Xunit;

namespace CurrencyExchange.UnitTests.Services
{
    [Trait(COLLECTION_NAME, COLLECTION_DESCRIPTION)]
    [Collection(DEFINITION_NAME)]
    public class MathServiceTests
    {
        const string COLLECTION_NAME = "Math service tests";
        const string COLLECTION_DESCRIPTION = "Testing proper functioning of calculating methods";
        const string DEFINITION_NAME = "unit-tests";

        [Fact(DisplayName = "Average Count")]
        public void AverateCount_Test()
        {
            // Arrange
            var averageValueExpected = 10m;
            var rates = new List<CurrencyRate>()
            {
                new CurrencyRate("USD", 10m),
                new CurrencyRate("USD", 5m),
                new CurrencyRate("USD", 15m)
            };

            // Act
            var averageValue = new MathService().GetAverateRate(rates);

            // Assert
            Assert.True(averageValue == averageValueExpected);
        }

        [Fact(DisplayName = "Maximum Count")]
        public void MaximumCount_Test()
        {
            // Arrange
            var maximumValueExpected = 15m;
            var rates = new List<CurrencyRate>()
            {
                new CurrencyRate("USD", 10m),
                new CurrencyRate("USD", 5m),
                new CurrencyRate("USD", 15m)
            };

            // Act
            var maximumValue = new MathService().GetMaximumRate(rates);

            // Assert
            Assert.True(maximumValue == maximumValueExpected);
        }

        [Fact(DisplayName = "Minimum Count")]
        public void MinimumCount_Test()
        {
            // Arrange
            var minimumValueExpected = 5m;
            var rates = new List<CurrencyRate>()
            {
                new CurrencyRate("USD", 10m),
                new CurrencyRate("USD", 5m),
                new CurrencyRate("USD", 15m)
            };

            // Act
            var minimumValue = new MathService().GetMinimumRate(rates);

            // Assert
            Assert.True(minimumValue == minimumValueExpected);
        }
    }
}
